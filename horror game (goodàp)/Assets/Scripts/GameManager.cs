using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //singleton logic
    private GameManager() { }

    private static GameManager _instance;

    private static readonly object threadSafeLock = new object();

    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            lock (threadSafeLock)
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
            }
        }
        return _instance;
    }



    [Header("Transforms")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform playerPos;
    [SerializeField] float minimumSpawnDistance = 15f;

    [Header("Enemies and player")]
    [SerializeField] GameObject stalker;
    [SerializeField] GameObject jeeper;
    [SerializeField] GameObject brute;
    [SerializeField] GameObject player;


    PlayerManager playerManager;

    public bool playerIsDead = false;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }



    public void StartNewGame()
    {
        SceneManager.LoadScene("HandScene");
        playerManager.ResetValues();
        playerManager.TeleportPlayerToRandomTarget();
    }
    public void EndGame(int picCount, int medCount, bool playerDied)
    {
        //player.SetActive(false);//disable player

        if (playerDied) //update playerdied for text at the end
        {
            playerIsDead = true;
        }
        else
        {
            playerIsDead = false;
        }

        CalculateScore(picCount, medCount, playerDied); //wirte IO

        SceneManager.LoadScene("EndScene");
    }
    
    public Transform GenerateEnemySpawnPoint()
    {
        Transform randomSpawnPoint;
        do
        {
            randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Debug.Log($"distance was: {Vector3.Distance(playerPos.position, randomSpawnPoint.position)}   the cube: {randomSpawnPoint}");

        } while (Vector3.Distance(playerPos.position, randomSpawnPoint.position) < minimumSpawnDistance);

        return randomSpawnPoint;
    }

    public void CalculateScore(int picsCount, int medsCount, bool playerDied)
    {
        Debug.Log("CalculateScore() script called");
        int score = 0;
        
        for (int i = 0; i < picsCount; i++)
        {
            score += 1000;
        }
        for (int i = 0; i < medsCount; i++)
        {
            score += 500;
        }
        if (!playerDied)
        {
            score += 5000;
        }

        FileManager.GetInstance().WriteAllData(score, playerDied);
    }
}

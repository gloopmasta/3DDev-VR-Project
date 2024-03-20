using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteController : MonoBehaviour
{
    [Header("Behaviour Scripts")]
    [SerializeField] RunToNextPoint runToNextPointScript;
    [SerializeField] Countdown countdownScript;
    [Space(10)]

    [SerializeField] float countdownTime = 2f;
    private float timeSinceLastActivation = 0f;

    [Header("Transforms")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform playerPos;
    [SerializeField] float minimumSpawnDistance = 15f;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();

        StartCoroutine(CycleScripts());
    }

    private void OnEnable()
    {
        Transform randomSpawnPoint;
        do
        {
            randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Debug.Log($"distance was too short: {Vector3.Distance(playerPos.position, randomSpawnPoint.position)}   the cube: {randomSpawnPoint}");

        } while (Vector3.Distance(playerPos.position, randomSpawnPoint.position) < minimumSpawnDistance);

        transform.position = randomSpawnPoint.position;
        Debug.Log($"SPAWNPOINT SET, distance: {Vector3.Distance(playerPos.position, randomSpawnPoint.position)}");

    }

    private IEnumerator CycleScripts()
    {
        while (true)
        {
            // Enable the Countdown script for 2 seconds
            countdownScript.enabled = true;
            yield return new WaitForSeconds(2f);

            // Disable the Countdown script
            countdownScript.enabled = false;

            // Enable the RunToNextPoint script until it disables itself
            runToNextPointScript.enabled = true;
            yield return new WaitUntil(() => !runToNextPointScript.enabled);
        }
    }
}

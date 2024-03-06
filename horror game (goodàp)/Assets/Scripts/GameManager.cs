using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void CalculateScore(int picsCount, int medsCount, bool isAlive)
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
        if (isAlive)
        {
            score += 5000;
        }

        FileManager.GetInstance().WriteAllData(score);
    }
}

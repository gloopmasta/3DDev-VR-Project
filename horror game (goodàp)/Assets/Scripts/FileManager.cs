using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager
{
    //singleton logic
    private FileManager() { }

    private static FileManager _instance;

    private static readonly object threadSafeLock = new object();

    public static FileManager GetInstance()
    {
        if (_instance == null)
        {
            lock (threadSafeLock)
            {
                if (_instance == null)
                {
                    _instance = new FileManager();
                }
            }
        }
        return _instance;
    }


    //IO
    public void WriteAllData(int score)
    {
        Debug.Log("WriteAllData() script called");
        //PREVIOUS SCORE
        if (PlayerPrefs.HasKey("Current Score"))
        {
            PlayerPrefs.SetInt("Previous Score", PlayerPrefs.GetInt("Current Score")); //Previous score becomes the last current score
        }
        else //if it's not the first time you played (current score doesn't exist yet)
        {
            PlayerPrefs.SetInt("Previous Score", 0); //make previous score 0
        }

        
        //CURRENT SCORE
        PlayerPrefs.SetInt("Current Score", score);


        //HIGH SCORE
        if (!PlayerPrefs.HasKey("High Score")) //if high score doesn't exist yet, make a highscore with 0 as value
        {
            PlayerPrefs.SetInt("High Score", score);
        }

        if (PlayerPrefs.GetInt("High Score") < PlayerPrefs.GetInt("Current Score")) //if currentscore bigger than highscore
        {
            PlayerPrefs.SetInt("High Score", PlayerPrefs.GetInt("Current Score"));//set highscore to currentscore value
        }

        PlayerPrefs.Save();
        Debug.Log("Previous Score: " + PlayerPrefs.GetInt("Previous Score"));
        Debug.Log("Current Score: " + PlayerPrefs.GetInt("Current Score"));
        Debug.Log("High Score: " + PlayerPrefs.GetInt("High Score"));
    }
}

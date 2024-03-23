using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoresText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI diedText;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Last score should be: " + PlayerPrefs.GetInt("Last Score"));
        Debug.Log("Current score should be: " + PlayerPrefs.GetInt("Current Score"));


        highScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("High Score");
        currentScoreText.text = "CURRENT SCORE " + PlayerPrefs.GetInt("Current Score");
        lastScoreText.text = "LAST SCORE " + PlayerPrefs.GetInt("Last Score");
        diedText.text = "status";

        if (PlayerPrefs.GetString("Player Status") == "dead")
        {
            diedText.text = "you died";
            diedText.color = Color.red;
        }
        else if (PlayerPrefs.GetString("Player Status") == "alive")
        {
            diedText.text = "you won";
            diedText.color = Color.green;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (diedText.text == "status")
        //{
        //    if (PlayerPrefs.GetString("Player Status") == "dead")
        //    {
        //        diedText.text = "you died";
        //        diedText.color = Color.red;
        //    }
        //    else if (PlayerPrefs.GetString("Player Status") == "alive")
        //    {
        //        diedText.text = "you won";
        //        diedText.color = Color.green;
        //    }
        //}
    }

}

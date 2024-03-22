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

    GameManager gameManager;

    void Start()
    {
        highScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("High Score");
        currentScoreText.text = "CURRENT SCORE " + PlayerPrefs.GetInt("Current Score");
        lastScoreText.text = "LAST SCORE " + PlayerPrefs.GetInt("Last Score");

        gameManager = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.playerIsDead)
        {
            diedText.text = "YOU DIED";
            diedText.color = Color.red;
        }
        else
        {
            diedText.text = "YOU WON";

            diedText.color = Color.green;
        }
    }

    public void Configuretext()
    {
        
    }
}

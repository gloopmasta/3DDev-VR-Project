using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoresText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = "HIGH SCORE " + PlayerPrefs.GetInt("High Score");
        currentScoreText.text = "CURRENT SCORE " + PlayerPrefs.GetInt("Current Score");
        lastScoreText.text = "LAST SCORE " + PlayerPrefs.GetInt("Last Score");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Configuretext()
    {
        
    }
}

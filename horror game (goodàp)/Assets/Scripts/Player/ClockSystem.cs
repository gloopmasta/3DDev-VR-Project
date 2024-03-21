using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;

public class ClockSystem : MonoBehaviour
{
    //[SerializeField]private PlayerManager playerManager;

    [SerializeField] private TextMeshProUGUI timeText;
    

    void Start()
    {
        //timeText = FindObjectOfType<TextMeshProUGUI>();
        //dreaming = true;
        //Debug.Log("time script started");

        timeText.text = "";
    }

    void Update()
    { 
    }

    public void DisplayTime(PlayerState state)
    {
        timeText.text = GenerateTime(state);
        //StartCoroutine(TimeHold(2)); //See if I can put function above into the timehold thing for less lines
    }

    public void ShutOffWatch()
    {
        timeText.text = "";
    }

    string GenerateTime(PlayerState state)
    {
        if (state != PlayerState.Dreaming)
        {
            return Random.Range(0, 2).ToString() + Random.Range(0, 9).ToString() + ":" + Random.Range(0, 6).ToString() + Random.Range(0, 9);
        }
        else
        {
            int messedUpPos = Random.Range(1, 2);

            switch (messedUpPos)
            {
                case 1:
                    return Random.Range(3, 6).ToString() + Random.Range(0, 9).ToString() + ":" + Random.Range(0, 9).ToString() + Random.Range(0, 9);

                case 2:
                    return Random.Range(0, 2).ToString() + Random.Range(0, 9).ToString() + ":" + Random.Range(7, 9).ToString() + Random.Range(0, 9);

                default:
                    return Random.Range(0, 2).ToString() + Random.Range(0, 9).ToString() + ":" + Random.Range(7, 9).ToString() + Random.Range(0, 9);
            }
        }
        
    }

    IEnumerator TimeHold(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        timeText.text = "";
    }
}

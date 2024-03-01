using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;

public class ClockSystem : MonoBehaviour
{
    [SerializeField]private PlayerManager playerManager;

    [SerializeField] private TextMeshProUGUI timeText;
    //string time;
    //bool dreaming;

    // Start is called before the first frame update
    void Start()
    {
        //timeText = FindObjectOfType<TextMeshProUGUI>();
        //dreaming = true;
        //Debug.Log("time script started");

        timeText.text = "";
    }

    // Update is called once per frame
    void Update()
    { 
        //Debug.Log(GenerateTime(true));
        //dreaming = !dreaming;

        //if (Input.GetKeyDown("up"))
        //{
        //    dreaming = !dreaming;
        //}
        //if (Input.GetKey("down"))
        //{
        //    timeText.text = GenerateTime(playerManager.isDreaming);
        //}
    }

    public void DisplayTime(bool isDreaming)
    {
        Debug.Log("DisplayTime script called");
        timeText.text = GenerateTime(isDreaming);
        StartCoroutine(TimeHold(2)); //See if I can put function above into the timehold thing for less lines
    }

    string GenerateTime(bool dreaming)
    {
        if (!dreaming)
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

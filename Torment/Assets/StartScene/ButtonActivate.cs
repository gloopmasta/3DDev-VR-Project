using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartScene()
    {
        Debug.Log("Load new scene");
        GameManager.GetInstance().StartNewGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

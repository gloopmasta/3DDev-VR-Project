using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedScript : MonoBehaviour
{
    PlayerManager manager;
    private void Start()
    {
        manager = PlayerManager.GetInstance();
    }
    private void OnTriggerEnter(Collider other)
    {
        manager.MedsCollected();
        Debug.Log("Med collected");
    }
}

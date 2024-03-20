using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Countdown : MonoBehaviour
{
    [SerializeField] float countdownTime = 2f;
    private float timeSinceLastActivation = 0f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.destination = transform.position;

        timeSinceLastActivation = 0f;
        
    }


    void Update()
    {
        timeSinceLastActivation += Time.deltaTime;

        if (timeSinceLastActivation >= countdownTime)
        {
            
            enabled = false;
        }
    }
}

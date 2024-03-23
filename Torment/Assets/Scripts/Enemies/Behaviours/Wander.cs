using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Wander : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private float wanderSpeed = 1.0f;
    private NavMeshAgent agent;
    private int currentTargetIndex;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTargetIndex = Random.Range(0, targets.Length);

        // Set initial destination
        agent.destination = targets[currentTargetIndex].position;
        agent.speed = wanderSpeed; // Set initial speed
    }

    void Update()
    {
        agent.speed = wanderSpeed;

        if (agent.enabled)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                MoveToRandomDestination();
            }
        }
    }

    void MoveToRandomDestination()
    {
        int newIndex = Random.Range(0, targets.Length);
        currentTargetIndex = newIndex;
        agent.destination = targets[currentTargetIndex].position; // Set destination to a random waypoint
    }

    private void OnDisable()
    {
        if (agent != null)
        {
            //Reset the NavMeshAgent's destination to stop movement
            agent.destination = transform.position;
        }
    }
}

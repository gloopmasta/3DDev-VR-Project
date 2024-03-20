using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunToNextPoint : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float waypointCheckDistance = 1.0f;
    [SerializeField] private float AccelSpeed = 50.0f;
    [SerializeField] private float StoppingDistance = 1.0f;
    [SerializeField] private float TurnSpeed = 1.0f;
    private NavMeshAgent agent;
    private int currentTargetIndex;
    private Transform lastTarget;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextRandomDestination();
    }

    void Update()
    {
        if (agent.enabled && !agent.pathPending)
        {
            for (int i = 0; i < targets.Length; i++) //check for every waypoint
            {
                if (Vector3.Distance(transform.position, targets[i].position) < waypointCheckDistance && targets[i] != lastTarget && targets[i] != targets[currentTargetIndex]) //if you're close enough to the waypoint AND if the one ur on rn isnt the last one you were on AND
                {
                    agent.destination = transform.position;
                    lastTarget = targets[i];

                    Debug.Log($"a                                    reached a waypoint: {targets[i]}");

                    enabled = false;
                }
            }
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.destination = transform.position;
            lastTarget = targets[currentTargetIndex];

            Debug.Log($"a                                    reached a waypoint THAT WAS ORIGINALLY ITS TARGET: {targets[currentTargetIndex]}");

            enabled = false;
        }
    }

    void SetNextRandomDestination()
    {
        int newIndex = Random.Range(0, targets.Length);
        currentTargetIndex = newIndex;
        agent.speed = runSpeed;
        agent.acceleration = AccelSpeed;
        //agent.stoppingDistance = StoppingDistance;
        //agent.angularSpeed = TurnSpeed;
        agent.destination = targets[currentTargetIndex].position; // Set destination to a random waypoint
    }

    private void OnDisable()
    {
        agent.destination = transform.position;
    }
}

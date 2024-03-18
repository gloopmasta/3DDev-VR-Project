using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunToPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float runSpeed = 5f;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f; // Ignore vertical difference

            //Rotation
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); //smoothly rotate towards player

            //Move towards the player
            //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            agent.destination = player.position;
            agent.speed = runSpeed;
        }
    }
}

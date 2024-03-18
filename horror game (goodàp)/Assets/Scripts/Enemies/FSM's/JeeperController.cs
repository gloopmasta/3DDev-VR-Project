using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeeperController : MonoBehaviour
{
    [Header("Behaviour Scripts")]
    [SerializeField] Wander wanderScript;
    [Space(10)]

    [Header("Raycast")]
    [SerializeField] Transform player;
    [SerializeField] float maxSightDistance = 100f;
    [SerializeField] float raycastInterval = 0.5f;
    [Space(10)]

    [Header("Trigger zones")]
    [SerializeField] BoxCollider[] colliderArray;
    [Space(10)]

    private float timeSinceLastRaycast = 0f;

    private void Start()
    {
        wanderScript.enabled = true ;
    }

    private void Update()
    {
        // Update time since last raycast
        timeSinceLastRaycast += Time.deltaTime;

        // Perform raycast at specified interval
        if (timeSinceLastRaycast >= raycastInterval)
        {
            // Check if the player is within line of sight
            if (CanSeePlayer())
            {
                Debug.Log("I seeeee youu hahaha im evil as FUCKKKK >:))))))");
            }
            else
            {
                //Enable wander script
            }

            // Reset timer
            timeSinceLastRaycast = 0f;
        }
    }

    private bool CanSeePlayer()
    {
        // Calculate direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Raycast to check for obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, maxSightDistance))
        {
            // If raycast hits something...
            if (hit.collider.CompareTag("Player"))
            {
                // Check if the hit object is the player
                return true; // Player is in line of sight
            }
        }
        return false; // Player not in line of sight
    }
}

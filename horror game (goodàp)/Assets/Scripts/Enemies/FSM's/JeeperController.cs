using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeeperController : MonoBehaviour
{
    private PlayerManager playerManager;

    [Header("Behaviour Scripts")]
    [SerializeField] Wander wanderScript;
    [SerializeField] RunToPlayer runToPlayerScript;
    [Space(10)]

    [Header("Raycast")]
    [SerializeField] Transform player;
    [Space(10)]

    [Header("Zone system")]                                     //Zone Detection
    [SerializeField] private List<string> currentZones;


    private void Start()
    {
        playerManager = PlayerManager.Instance;
        currentZones = new List<string>();


        wanderScript.enabled = true;
        runToPlayerScript.enabled = false;
    }

    private void Update()
    {
        if (IsPlayerLookingAtEnemy() && runToPlayerScript.enabled)
        {
            Debug.Log("Player is looking at the enemy");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone")) //If enter new zone
        {
            currentZones.Add(other.gameObject.name); //update current zone
            Debug.Log($"{gameObject.name} entered zone: {currentZones[currentZones.Count - 1]}");
        }

        CheckSameZone();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone")) //If exit  zone
        {
            currentZones.Remove(other.gameObject.name); //Remove zone you just exited exitted?
        }
    }

    private bool CheckSameZone()
    {
        foreach (string enemyZone in currentZones)
        {
            foreach (string playerZone in playerManager.GetCurrentZones())
            {
                if (enemyZone == playerZone) //if any of the zones the player is in are equal to any of the zones the enemy is in
                {
                    Debug.Log("We're in the same zone rn, ... I might just touch you...");

                    //Enable run script
                    wanderScript.enabled = false;
                    runToPlayerScript.enabled = true;

                    return true;
                }
            }
        }
        return false;
    }

    private bool IsPlayerLookingAtEnemy()
    {
        // Calculate direction from player to enemy
        Vector3 directionToEnemy = transform.position - player.position;

        // Calculate angle between direction to enemy and player's forward vector
        float angleToEnemy = Vector3.Angle(player.forward, directionToEnemy);

        // Get the player's field of view angle
        float playerFOV = player.GetComponentInChildren<Camera>().fieldOfView;

        // Check if angle to enemy is within half of the player's FOV angle
        return angleToEnemy <= playerFOV * 0.5f;
    }

}

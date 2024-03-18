using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeeperController : MonoBehaviour
{
    private PlayerManager playerManager;

    [Header("Behaviour Scripts")]
    [SerializeField] Wander wanderScript;
    [SerializeField] RunToPlayer runToPlayerScript;
    [SerializeField] Freeze freezeScript;
    [Space(10)]

    [Header("idk")]
    [SerializeField] Transform player;
    [SerializeField] bool lockedOnToPlayer;
    [Space(10)]

    [Header("Zone system")]                                     //Zone Detection
    [SerializeField] private List<string> currentZones;


    private void Start()
    {
        playerManager = PlayerManager.Instance;
        currentZones = new List<string>();

        lockedOnToPlayer = false;

        wanderScript.enabled = true;
        runToPlayerScript.enabled = false;
        freezeScript.enabled = false;
    }

    private void Update()
    {
        if (lockedOnToPlayer)
        {
            RaycastHit hit;

            //raycast to player to see if enemy can see player
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit))
            {
                //check if the enemy can see the player
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("The player is currently in the enemy's sight");
                    if (IsPlayerLookingAtEnemy()) //if the player is looking at the enemy
                    {
                        runToPlayerScript.enabled = false;
                        freezeScript.enabled = true;
                    }
                    else
                    {
                        runToPlayerScript.enabled = true;
                        freezeScript.enabled = false;
                    }

                }
                else //if the raycast doesn't hit the player anymore
                {
                Debug.Log("The Enemy has lost sight of the player");
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone") && !lockedOnToPlayer) //If enter new zone and NOT LOCKED ON
        {
            currentZones.Add(other.gameObject.name); //update current zone
            Debug.Log($"{gameObject.name} entered zone: {currentZones[currentZones.Count - 1]}");

            CheckSameZone();
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone") && !lockedOnToPlayer) //If exit  zone AND NOT LOCKED ON
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
                    //Debug.Log("Enemy and player in the same zone");

                    //Enable run script
                    wanderScript.enabled = false;
                    runToPlayerScript.enabled = true;

                    lockedOnToPlayer = true;

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

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
    [SerializeField] Transform[] spawnPoints;
    [Space(10)]

    [Header("Zone system")]                                     //Zone Detection
    [SerializeField] private List<string> currentZones;


    [SerializeField] float timeInterval = 1f;
    private float timeSinceLastActivation = 0.0f;

    float minimumSpawnDistance = 20f;

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
        if (!lockedOnToPlayer) //check zones every second
        {
            timeSinceLastActivation += Time.deltaTime;//update timer

            if (timeSinceLastActivation >= timeInterval)
            {
                CheckSameZone(); //check same zone every second
                //Debug.Log("I'm checking the area rn");
                timeSinceLastActivation = 0.0f; //reset the times
            }
        }
        else //if lockedontoplayer
        {
            RaycastHit hit;

            Vector3 raycastOrigin = transform.position + Vector3.up; //transform of enemy + 1 unit up so it doesn't collide with the ground

            //raycast to player to see if enemy can see player
            if (Physics.Raycast(raycastOrigin, (player.position - transform.position).normalized, out hit))
            {
                //check if the enemy can see the player
                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Zone")) //if he sees a zone or player act normal
                {
                    //Debug.Log("LOOKIGN AT PLAYER (look-nolook behaviours should be working rn)");
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
                    //Debug.Log($"The Enemy sees: {hit.collider.gameObject.name} RUNNING TO PLAYER");
                    runToPlayerScript.enabled = true;
                    freezeScript.enabled = false;
                }
            }
        }


    }

    private void OnEnable()
    {
        Transform randomSpawnPoint;
        do
        {
            randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Debug.Log($"distance was too short: {Vector3.Distance(player.position, randomSpawnPoint.position)}   the cube: {randomSpawnPoint}");

        } while (Vector3.Distance(player.position, randomSpawnPoint.position) < minimumSpawnDistance);

        transform.position = randomSpawnPoint.position;
        Debug.Log($"SPAWNPOINT SET, distance: {Vector3.Distance(player.position, randomSpawnPoint.position)}");
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
        if (player == null)
        {
            return false; // If player Transform is null, return false
        }

        // Get the player's camera
        Camera playerCamera = player.GetComponentInChildren<Camera>();

        if (playerCamera == null)
        {
            return false; // If camera is null, return false
        }

        // Calculate direction from player camera to enemy
        Vector3 directionToEnemy = transform.position - playerCamera.transform.position;

        // Calculate angle between direction to enemy and camera's forward vector
        float angleToEnemy = Vector3.Angle(playerCamera.transform.forward, directionToEnemy);

        // Get the player's field of view angle
        float playerFOV = playerCamera.fieldOfView;

        // Check if angle to enemy is within half of the player's FOV angle
        return angleToEnemy <= playerFOV * 0.5f;
    }


}

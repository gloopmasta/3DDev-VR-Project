using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float fleeDistance = 2f;
    [SerializeField] float runAwayTime = 5f;
    [SerializeField] Transform[] spawnPoints;

    [Space(10)]

    [Header("Behaviour Scripts")]
    [SerializeField] Wander wanderScript;
    [SerializeField] Freeze freezeScript;
    [SerializeField] RunAwayFromPlayer runAwayScript;
    [Space(10)]

    [Header("Zone system")]                                     //Zone Detection
    [SerializeField] private List<string> currentZones;

    private PlayerManager playerManager;

    [SerializeField] float timeInterval = 2f;                   //tijd
    private float timeSinceLastActivation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.Instance;
        currentZones = new List<string>();

    }

    private void OnEnable()
    {
        wanderScript.enabled = true;
        freezeScript.enabled = false;
        runAwayScript.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (freezeScript.enabled)
        {
            //look at player
            transform.rotation = Quaternion.LookRotation(player.position - transform.position);
        }

        if (Vector3.Distance(player.position, transform.position) < fleeDistance) //is the player too close? -> run
        {
            StartCoroutine(RunAwayForSeconds());
        }
        else
        {
            //check zones every second
            timeSinceLastActivation += Time.deltaTime;//update timer

            if (timeSinceLastActivation >= timeInterval)
            {
                CheckSameZone(); //check same zone every second
                Debug.Log("I'm checking the area rn");
                timeSinceLastActivation = 0.0f; //reset the times
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone")) //If enter new zone
        {
            currentZones.Add(other.gameObject.name); //update current zone
            Debug.Log($"{gameObject.name} entered zone: {currentZones[currentZones.Count - 1]}");
        }

        if (other.CompareTag("Player")) //If collide with player -> teleport to random
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            other.gameObject.transform.position= new Vector3(randomPoint.position.x, player.position.y, randomPoint.position.z); //get only x and z of random point

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone")) //If exit  zone AND NOT LOCKED ON
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
                if (enemyZone == playerZone) //if enemy and player in same zone
                {
                    //Enable freeze script
                    wanderScript.enabled = false;
                    freezeScript.enabled = true;


                    return true;
                }
            }
        }

        Debug.Log("No longer in same hall -> wander instead of freeze");
        freezeScript.enabled = false;
        wanderScript.enabled = true;
        runAwayScript.enabled = false;

        return false;
    }

    private IEnumerator RunAwayForSeconds()
    {
        //all scripts false except runaway
        wanderScript.enabled = false;
        freezeScript.enabled = false;
        runAwayScript.enabled = true;

        yield return new WaitForSeconds(runAwayTime);

        runAwayScript.enabled = false; //start wandering again after 5 seconds
        wanderScript.enabled = true;
    }
}

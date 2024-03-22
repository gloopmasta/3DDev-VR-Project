using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerManager : MonoBehaviour
{
    //singleton
    private object lockThreadSafety = new object();
    private static PlayerManager instance = null;
    public static PlayerManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        lock (lockThreadSafety)
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    
    [Header("Scripts")]                                             //scripts
    [SerializeField] private ClockSystem clockScript;
    [SerializeField] private BatterySystem batteryScript;
    [Space(10)]

    [Header("Player Variables")]
    [SerializeField] private int batteryLevel;
    [SerializeField] public int pictureCount;
    [SerializeField] public int medsCount;
    [SerializeField] private PlayerState state;
    [Space(10)]

    [Header("Targets")]
    [SerializeField] Transform[] targets;
    [Space(10)]

    [Header("Child objects")]
    [SerializeField] GameObject rightHandMesh;
    [SerializeField] private GameObject med;
    [SerializeField] private GameObject knife;
    [Space(10)]

    [Header("Enemies")]
    [SerializeField] GameObject stalker;
    [SerializeField] GameObject jeeper;
    [SerializeField] GameObject brute;

    [Header("Zone system")]                                     //Zone Detection
    [SerializeField] private List<string> currentZones;

    private bool watchActive = false;
    GameManager gameManager;



    void Start()
    {
        //batteryLevel = 5;
        //medsCount = 2;
        //pictureCount = 0;
        //state = PlayerState.AwakeAndSane;

        ResetValues();
        TeleportPlayerToRandomTarget();


        gameManager = GameManager.GetInstance(); //instantiate gamemanager 
        currentZones = new List<string>();
    }
    
    
    public void ResetValues()
    {
        batteryLevel = 5;
        medsCount = 3;
        pictureCount = 0;
        state = PlayerState.AwakeAndSane;
    }

    public void TeleportPlayerToRandomTarget()
    {
        Transform randomPoint = targets[Random.Range(0, targets.Length)];

        transform.position = new Vector3(randomPoint.position.x, transform.position.y, randomPoint.position.z); //get only x and z of random point

        Debug.Log("POSITIE SPELER: " + transform.position);
        Debug.Log("CHOSEN POINT: " + randomPoint.position);

        //random rotation player
        transform.rotation = Quaternion.Euler(0, Random.rotation.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone")) //If enter new zone
        {
            //Update current zone
            currentZones.Add(other.gameObject.name); 
            Debug.Log($"{gameObject.name} entered zone: {currentZones[currentZones.Count - 1]}");


            //Chance to switch state
            if (state == PlayerState.AwakeAndSane)
            {
                int randomInt = Random.Range(1, 9);//  1/4 chance to switch state  either dreaming or hallucinating
                if (randomInt == 7)
                {
                    StateChange(PlayerState.Dreaming);
                }
                if (randomInt == 8)
                {
                    StateChange(PlayerState.Hallucinating);
                }
            }

        }
        if (other.CompareTag("Enemy")) //ENEMY COLLISION
        {
            gameManager.EndGame(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone")) //If exit new zone
        {
            currentZones.Remove(other.gameObject.name); //Remove zone you just exited exitted?
        }
    }
    public List<string> GetCurrentZones()
    {
        return currentZones;
    }

    public void StateChange(PlayerState newState)
    {
        state = newState;

        switch (state)
        {
            case PlayerState.AwakeAndSane:
                Debug.Log("stalker spawned");
                stalker.SetActive(true);
                jeeper.SetActive(false);
                brute.SetActive(false);
                return;
            case PlayerState.Dreaming:
                Debug.Log("Brute spawned");
                stalker.SetActive(false);
                jeeper.SetActive(false);
                brute.SetActive(true);
                return;
            case PlayerState.Hallucinating:
                Debug.Log("Jeeper spawned");
                stalker.SetActive(false);
                jeeper.SetActive(true);
                brute.SetActive(false);
                return;
        }
    }

    public void Die()
    {
        GameManager.GetInstance().CalculateScore(pictureCount, medsCount, false);
    }

    void CheckWatch()
    {
        if (!watchActive && batteryLevel > 0)
        {
            StartCoroutine(WatchLogic());
        }
    }

    IEnumerator WatchLogic()
    {
        watchActive = true;

        clockScript.DisplayTime(state);
        batteryLevel--;
        batteryScript.UpdateBatteryLevel(batteryLevel);

        yield return new WaitForSeconds(1);//wait A second

        clockScript.ShutOffWatch();
        watchActive = false;
    }
    //IEnumerator SoundHitboxActivate()
    //{
    //    sphereCollider.enabled = true;
    //    Debug.Log("hitbox enabled");
    //    yield return new WaitForSeconds(2);
    //    sphereCollider.enabled = false;
    //    Debug.Log("hitbox disabled");
    //}

    public void BatteryCollected()
    {
        batteryLevel++;
        batteryScript.UpdateBatteryLevel(batteryLevel);
    }


    
    public void MedsCollected()
    {
        Debug.Log("if something happens here thats good news");
        medsCount++;
    }

    public void PictureCollected()
    {
        pictureCount++;
        if (pictureCount >= 1)
        {
            Debug.Log("Game won");
            GameManager.GetInstance().CalculateScore(pictureCount, medsCount, true);
        }
    }

    //state switches
    public void TakeMed()
    {
        medsCount--;

        if (state == PlayerState.Hallucinating)
        {
            StateChange(PlayerState.AwakeAndSane);//alert the method that state has changed
        }
        else
        {
            Debug.Log("Player took a med but it had no effect! Meds: " + medsCount);
        }
    }
    public void Stab()
    {
        if (state == PlayerState.Dreaming)
        {
            StateChange(PlayerState.AwakeAndSane); //wake up if dreaming
        }
        else
        {
            gameManager.EndGame(true); //kill yourself if not
        }
    }


    private void OnLeftClick1()
    {
        CheckWatch();
    }
    private void OnLeftClick2()
    {
        CheckWatch();
    }


    //                                                              RIGHT BUTTON 1
    public void OnRightClick1Pressed()
    {
        if (!med.activeInHierarchy)
        {
            //rightHandMesh.SetActive(false);
            knife.SetActive(true);
        }
    }

    public void OnRightClick1Released()
    {
        //rightHandMesh.SetActive(true);
        knife.SetActive(false);
    }



    //                                                              RIGHT BUTTON 2
    public void OnRightClick2Pressed()
    {
        if (medsCount > 0 && !knife.activeInHierarchy)
        {
            //rightHandMesh.SetActive(false);
            med.SetActive(true);
        }
    }

    public void OnRightClick2Released()
    {
        //rightHandMesh.SetActive(true);
        med.SetActive(false);
    }

}

public enum PlayerState
{
    AwakeAndSane,
    Hallucinating,
    Dreaming
}

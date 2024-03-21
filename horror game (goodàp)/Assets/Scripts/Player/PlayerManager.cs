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
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private PlayerState state;
    [Space(10)]

    [Header("Prefabs")]
    [SerializeField] private GameObject med;
    [SerializeField] private GameObject knife;
    [Space(10)]

    [Header("Child objects")]
    [SerializeField] GameObject rightHandMesh;
    [Space(10)]

    [Header("Zone system")]                                     //Zone Detection
    [SerializeField] private List<string> currentZones;

    private bool watchActive = false;


    void Start()
    {
        batteryLevel = 5;
        medsCount = 2;
        pictureCount = 0;
        state = PlayerState.AwakeAndSane;

        currentZones = new List<string>();

        sphereCollider.enabled = false;
    }
    
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone")) //If enter new zone
        {
            currentZones.Add(other.gameObject.name); //update current zone
            Debug.Log($"{gameObject.name} entered zone: {currentZones[currentZones.Count - 1]}");
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("                                             player is krill");
            GameManager.GetInstance().LoadScene("StartScene");
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
    IEnumerator SoundHitboxActivate()
    {
        sphereCollider.enabled = true;
        Debug.Log("hitbox enabled");
        yield return new WaitForSeconds(2);
        sphereCollider.enabled = false;
        Debug.Log("hitbox disabled");
    }

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

        switch (state)
        {
            case PlayerState.AwakeAndSane:
                Debug.Log("You're not hallucinating, the medication had no effect!");
                return;
            case PlayerState.Dreaming:
                Debug.Log("You're dreaming, the medication had no effect!");
                return;
            case PlayerState.Hallucinating:
                Debug.Log("You're hallucinating, the medication worked!");
                state = PlayerState.AwakeAndSane;
                return;
        }
    }
    public void Stab()
    {
        switch (state)
        {
            case PlayerState.AwakeAndSane:
                
                return;
            case PlayerState.Hallucinating:
                
                return;
            case PlayerState.Dreaming:
                state = PlayerState.AwakeAndSane;
                return;
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

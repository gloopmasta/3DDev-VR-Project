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

    //Scripts
    [SerializeField] private ClockSystem clockScript;
    [SerializeField] private BatterySystem batteryScript;
    
    
    //Player variables
    [SerializeField] protected int _batteryLevel;
    [SerializeField] public int pictureCount;
    [SerializeField] public int medsCount;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private PlayerState state;
    public int BatteryLevel
    {
        get{ return _batteryLevel; }
        set
        {
            if (value < 0)
            {
                _batteryLevel = 0;
            }
            else if (value > 5)
            {
                _batteryLevel = 5;
            }
            else
            {
                _batteryLevel = value;
            }
        }
    }
    

    //input
    [SerializeField] InputActionProperty pinchAnimationOnAction;

    void Start()
    {
        BatteryLevel = 5;
        medsCount = 2;
        pictureCount = 0;
        state = PlayerState.AwakeAndSane;

        sphereCollider.enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("M1 Pressed: checkwatch");
            CheckWatch();
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            Debug.Log("M2 Pressed enabled dreaming");
            state = PlayerState.Dreaming;
        }

        float triggervalue = pinchAnimationOnAction.action.ReadValue<float>();
        if (triggervalue > 0.9)
        {

        }
    }

    public void Die()
    {
        GameManager.GetInstance().CalculateScore(pictureCount, medsCount, false);
    }

    void CheckWatch()
    {
        clockScript.DisplayTime(state);
        StartCoroutine(SoundHitboxActivate());
        BatteryLevel--;
        batteryScript.UpdateBatteryLevel(BatteryLevel);
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
        BatteryLevel++;
        batteryScript.UpdateBatteryLevel(BatteryLevel);
    }


    void MedsUsed()
    {
        medsCount--;
    }
    public void MedsCollected()
    {
        Debug.Log("if something happens here thats good news");
        //MedsCount++;
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
        switch (state)
        {
            case PlayerState.AwakeAndSane:
                Debug.Log("You're not hallucinating, the medication had no effect!");
                return;
            case PlayerState.Dreaming:
                Debug.Log("You're dreaming, the medication had no effect!");
                return;
            case PlayerState.Hallucinating:
                Debug.Log("You're dreaming, the medication had no effect!");
                return;
        }
    }
    public void Stab()
    {

    }
}

public enum PlayerState
{
    AwakeAndSane,
    Hallucinating,
    Dreaming
}

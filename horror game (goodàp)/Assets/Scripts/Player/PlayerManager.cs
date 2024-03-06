using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerManager : MonoBehaviour
{
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
    [SerializeField] protected int _pictureCount;
    [SerializeField] private int _medsCount;
    [SerializeField] private SphereCollider _sphereCollider;
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
    public int MedsCount
    {
        get { return _medsCount; }
        set
        {
            if (value < 0)
            {
                _medsCount = 0;
            }
            else
            {
                _medsCount = value;
            }
        }
    }

    //input
    [SerializeField] InputActionProperty pinchAnimationOnAction;

    void Start()
    {
        //yButton.Enable();
        BatteryLevel = 5;
        MedsCount = 2;
        _pictureCount = 0;
        //isDreaming = false;
        //isHallucinating = false;
        state = PlayerState.AwakeAndSane;

        _sphereCollider.enabled = false;
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

    void CheckWatch()
    {
        clockScript.DisplayTime(state);
        StartCoroutine(SoundHitboxActivate());
        BatteryLevel--;
        batteryScript.UpdateBatteryLevel(BatteryLevel);
    }
    IEnumerator SoundHitboxActivate()
    {
        _sphereCollider.enabled = true;
        Debug.Log("hitbox enabled");
        yield return new WaitForSeconds(2);
        _sphereCollider.enabled = false;
        Debug.Log("hitbox disabled");
    }

    public void BatteryCollected()
    {
        BatteryLevel++;
        batteryScript.UpdateBatteryLevel(BatteryLevel);
    }


    void MedsUsed()
    {
        MedsCount--;
    }
    public void MedsCollected()
    {
        Debug.Log("if something happens here thats good news");
        //MedsCount++;
        _medsCount++;
    }

    public void PictureCollected()
    {
        _pictureCount++;
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

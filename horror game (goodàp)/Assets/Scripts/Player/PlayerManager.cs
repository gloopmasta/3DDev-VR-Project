using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerManager : MonoBehaviour
{
    //Scripts
    [SerializeField] private ClockSystem clockScript;
    [SerializeField] private BatterySystem batteryScript;
    
    
    //Player variables
    [SerializeField] protected int _batteryLevel;
    [SerializeField] private int _medsCount;
    [SerializeField] private SphereCollider _sphereCollider;

    //input
    [SerializeField]XRController leftController;
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

    public bool isDreaming;

    public bool isHallucinating;

    public bool IsNormal
    {
        get
        {
            if (!isDreaming && !isHallucinating)
            { 
                return true; 
            }
            else { return false; }
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

    void Start()
    {
        //yButton.Enable();
        BatteryLevel = 5;
        MedsCount = 3;
        isDreaming = false;
        isHallucinating = false;

        _sphereCollider.enabled = false;
    }
    
    void Update()
    {
        //bool yButtonPressed = yButton.action.ReadValue<bool>();
        if (leftController)
        {
            if (OVRInput.Get(OVRInput.Button.Three, OVRInput.Controller.LTouch))
            {
                Debug.Log("Y Pressed");
            }
        }
        

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("M1 Pressed");
            CheckWatch();
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            Debug.Log("M2 Pressed Toggle dreamig");
            isDreaming = !isDreaming;
        }
    }

    void CheckWatch()
    {
        clockScript.DisplayTime(isDreaming);
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

    void BatteryCollected()
    {
        BatteryLevel++;
        batteryScript.UpdateBatteryLevel(BatteryLevel);
    }


    void MedsUsed()
    {
        MedsCount--;
    }
    void MedsCollected()
    {
        MedsCount++;
    }
}

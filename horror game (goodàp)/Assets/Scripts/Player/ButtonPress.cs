using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static OVRInput;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] Controller _controller;
    [SerializeField] bool pressYRaw;
    void Start()
    {
        
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("Button 1 pressed");
        }
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            Debug.Log("Button 2 pressed");
        }

        pressYRaw = OVRInput.Get(OVRInput.RawButton.Y);

        if (pressYRaw)
        {
            Debug.Log("Y pressed");
        }
    }
}

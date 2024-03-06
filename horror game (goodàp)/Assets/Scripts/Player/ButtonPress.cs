using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static OVRInput;

public class ButtonPress : MonoBehaviour
{
    //[SerializeField] Controller _controller;
    //[SerializeField] bool pressYRaw;
    void Start()
    {
        
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("b1HET WERKT!!!!!!");
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("2HET WERKT!!!!!!");
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("3HET WERKT!!!!!!");
        }
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("4HET WERKT!!!!!!");
        }
    }
}

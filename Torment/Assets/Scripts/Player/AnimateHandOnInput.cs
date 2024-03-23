using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField]
    InputActionProperty pinchAnimationOnAction;
    
    [SerializeField]
    InputActionProperty gripAnimationOnAction;

    [SerializeField] private Animator handAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggervalue = pinchAnimationOnAction.action.ReadValue<float>();
        
        handAnimator.SetFloat("Trigger", triggervalue);

        float gripValue = gripAnimationOnAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}

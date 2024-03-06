using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WatchTouch : MonoBehaviour
{
    [SerializeField]
    SphereCollider pinchBox;

    [SerializeField]
    InputActionProperty pinchAnimationOnAction;

    // Start is called before the first frame update
    void Start()
    {
        pinchBox = gameObject.GetComponent<SphereCollider>();
        pinchBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float triggervalue = pinchAnimationOnAction.action.ReadValue<float>();
        if (triggervalue > 0.9)
        {
            pinchBox.enabled = true;
            Debug.Log("pinchbox enabled");
        }
        else
        {
            pinchBox.enabled = false;
            Debug.Log("pinchbox disabled");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == gameObject.CompareTag("Watch"))
        {
            Debug.Log("JOEPIEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        }
    }
}

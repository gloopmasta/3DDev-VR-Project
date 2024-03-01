using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheelchair : MonoBehaviour
{
    [SerializeField] private Transform xROriginTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotation of wheelchair is the same as the character
        gameObject.transform.rotation = xROriginTransform.rotation;
    }
}

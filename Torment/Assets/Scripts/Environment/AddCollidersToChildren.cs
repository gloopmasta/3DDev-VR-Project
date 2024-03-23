using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCollidersToChildren : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
                {
                    // Add a Mesh Collider to each child object
                    MeshCollider collider = child.gameObject.AddComponent<MeshCollider>();
        
                    // Ensure the collider is convex for accurate collision detection
                    collider.convex = true;
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedScript : MonoBehaviour
{
    PlayerManager manager;
    Transform cameraTransform;

    private void Start()
    {
        manager = PlayerManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(this.transform.position, cameraTransform.position);
        Debug.Log(distance);

        if (distance <= 0.2)
        {
            manager.MedsCollected();
            Debug.Log("Med collected");
            Destroy(gameObject);
        }
    }
}

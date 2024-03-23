using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMedScript : MonoBehaviour
{
    PlayerManager manager;
    Transform cameraTransform;

    private void Start()
    {
        manager = PlayerManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, cameraTransform.position);

        if (distance <= 0.2)
        {
            manager.TakeMed();
            Debug.Log("Med collected");
            gameObject.SetActive(false);
        }

    }
}

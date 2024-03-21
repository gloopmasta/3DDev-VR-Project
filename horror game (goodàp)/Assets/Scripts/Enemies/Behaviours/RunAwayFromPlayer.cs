using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayFromPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float runSpeed = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = transform.position - player.position;
        directionToPlayer.y = 0f; //Ignore vertical difference

        //Rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); //smoothly rotate towards player

        //Move
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }
}

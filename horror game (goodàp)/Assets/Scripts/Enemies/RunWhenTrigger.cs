using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RunWhenTrigger : MonoBehaviour
{
    [SerializeField] Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        //_animator.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ENEMY: I just heard a sound...");
        _animator.SetBool("isRunning", true);
    }
    
    
}

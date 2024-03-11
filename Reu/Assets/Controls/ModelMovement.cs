using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{

    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Rigidbody rb;
    
    
    // Update is called once per frame
    void Update()
    {
        var velocity = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")* speed);
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        
        


    }
}

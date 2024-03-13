using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics;

public class ModelMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private ArticulationBody rootBody;

    void Update()
    {
        MoveModel();
    }

    private void MoveModel()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
        }

        if (Input.GetKey(KeyCode.R))
        {
            RotateModel(rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            RotateModel(-rotationSpeed * Time.deltaTime);
        }

        // Calculate the new position based on the input and speed
        Vector3 newPosition = rootBody.transform.position + movement * (speed * Time.deltaTime);

        // Teleport the root to the new position
        TeleportRoot(newPosition, rootBody.transform.rotation);
    }

    private void RotateModel(float rotationAmount)
    {
        // Calculate the new rotation based on the input rotation amount
        Quaternion newRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        
        // Set the new rotation directly on the root body
        rootBody.transform.rotation *= newRotation;
    }

    // Your existing TeleportRoot function
    private void TeleportRoot(Vector3 position, Quaternion rotation)
    {
        rootBody.TeleportRoot(position, rotation);
    }
}
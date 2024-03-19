using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    [SerializeField] private GameObject modelGameObject;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private ArticulationBody rootBody;
    [SerializeField] private GameObject animatorGameObject; // Reference to the GameObject with the animator component
    private Animator animator;

    private void Start()
    {
        // Find the animator component in the specified GameObject
        if (animatorGameObject != null)
        {
            animator = animatorGameObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Animator GameObject reference is not set in ModelMovement script.");
        }

        animator.speed = 0;
    }

    void Update()
    {
        MoveModel();
    }

    private void MoveModel()
    {
        Vector3 movement = Vector3.zero;

        // Calculate movement based on input
        movement = Movement(movement);

        // Calculate the new position based on the input and speed
        Vector3 newPosition = rootBody.transform.position + movement * (speed * Time.deltaTime);

        // Teleport the root to the new position
        rootBody.TeleportRoot(newPosition, rootBody.transform.rotation);

        // Rotate the GameObject based on input
        RotateModel();
    }

    private Vector3 Movement(Vector3 movement)
    {
        // Get the forward and right directions relative to the object's rotation
        Vector3 forwardDirection = modelGameObject.transform.forward;
        Vector3 rightDirection = modelGameObject.transform.right;

        // Reset the y component to zero to ensure movement is only along the X-Z plane
        forwardDirection.y = 0f;
        rightDirection.y = 0f;

        // Normalize to ensure consistent speed regardless of direction
        forwardDirection.Normalize();
        rightDirection.Normalize();

        // Calculate movement based on input
        if (Input.GetKey(KeyCode.A))
        {
            movement += rightDirection; // Move left relative to the object's right direction
            animator.speed = 1;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            animator.speed = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement -= rightDirection; // Move right relative to the object's right direction
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movement -= forwardDirection; // Move forward relative to the object's forward direction
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement += forwardDirection; // Move backward relative to the object's forward direction
            animator.speed = 1;
        }

        return movement;
    }






    private void RotateModel()
    {
        // Rotate the modelGameObject based on input
        if (Input.GetKey(KeyCode.Q))
        {
            modelGameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            modelGameObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 movement;
    [SerializeField] public float movementSpeedFactor = 1f;
    Rigidbody rb;
    Animator animator;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * movementSpeedFactor, 0, movement.y * movementSpeedFactor);
        animator.SetBool("moving", rb.velocity.magnitude > 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private float groundCheckRadius = 0.2f;

    private Vector2 moveInput;
    private bool jumpInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Player movement using Input System
        Vector3 movement = new Vector3(moveInput.x, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Jumping using Input System
        if (isGrounded && jumpInput)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        moveInput = new Vector2(inputVector.x, 0.0f); // Only capture horizontal input
    }

    public void OnJump(InputValue value)
    {
        jumpInput = value.isPressed;
    }
}
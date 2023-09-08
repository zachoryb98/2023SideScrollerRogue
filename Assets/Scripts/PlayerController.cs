using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Vector2 moveInput;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform; // Cache the player's transform component.
    }

    private void Update()
    {
        // Player movement using Input System
        Vector3 movement = new Vector3(moveInput.x, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;
        playerTransform.Translate(movement);

        // Rotate the player based on input
        if (moveInput.x < 0)
        {
            // If moving left (A key pressed), rotate to face left
            playerTransform.localScale = new Vector3(-1, 1, 1); // Flip the player horizontally
        }
        else if (moveInput.x > 0)
        {
            // If moving right (D key pressed), rotate to face right
            playerTransform.localScale = new Vector3(1, 1, 1); // Reset the scale to face right
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
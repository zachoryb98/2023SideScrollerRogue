using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private Vector2 moveInput;

    private void Update()
    {
        // Player movement using Input System
        Vector3 movement = new Vector3(moveInput.x, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // Add debug log to check if this method is being called.
        Debug.Log("Move input: " + moveInput);
    }
}
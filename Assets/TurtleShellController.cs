using UnityEngine;

public class TurtleShellController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float leftOffset = -10.0f; // Offset from initial position for the left point
    public float rightOffset = 10.0f; // Offset from initial position for the right point
    public float rotationSpeed = 90.0f;
    public float rotationDelay = 1.0f; // Delay in seconds before rotating

    private Vector3 startPoint;
    private Vector3 endPoint;
    private Quaternion targetRotation;
    private bool isMovingRight = true; // Flag to control movement direction
    private bool isWaiting = false; // Flag to control waiting period
    private float waitTimer = 0.0f;
    Animator animator;

    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        startPoint = transform.position + Vector3.right * leftOffset;
        endPoint = transform.position + Vector3.right * rightOffset;
        targetRotation = Quaternion.identity; // Initialize the target rotation
    }

    private void Update()
    {
        if (isWaiting)
        {
            // Increment the wait timer
            waitTimer += Time.deltaTime;

            // Check if the waiting period is over
            if (waitTimer >= rotationDelay)
            {
                isWaiting = false;
                animator.SetBool("IsMoving", true);

                waitTimer = 0.0f;
                Rotate();
            }
        }
        else
        {
            // Calculate the move direction based on the movement flag
            Vector3 moveDirection = isMovingRight ? Vector3.right : Vector3.left;

            // Move towards the current target
            float step = moveSpeed * Time.deltaTime;
            transform.position += moveDirection * step;

            // Check if we have reached the current target
            if (Vector3.Distance(transform.position, (isMovingRight ? endPoint : startPoint)) < 0.1f)
            {
                // Start the waiting period
                isWaiting = true;
                animator.SetBool("IsMoving", false);
            }
        }
    }

    private void Rotate()
    {
        // Rotate the enemy
        float rotationAmount = isMovingRight ? 180.0f : -180.0f;
        float elapsedTime = 0.0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0.0f, rotationAmount, 0.0f);

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime);
        }

        // Toggle the movement direction flag
        isMovingRight = !isMovingRight;
    }
}

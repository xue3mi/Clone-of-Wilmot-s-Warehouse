using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float characterSpeed = 5f;       // Movement speed
    public float smoothTime = 0.1f;         // Smoothing time for SmoothDamp

    private Vector2 targetPosition;         // Target position to move toward
    private Vector2 velocity = Vector2.zero; // Velocity reference for smoothing

    private void Start()
    {
        // Set initial target position to current position
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Get input direction from keys (WASD)
        Vector2 inputDirection = GetMovementInput();

        // Only update target position if input is pressed
        if (inputDirection != Vector2.zero)
        {
            Move(inputDirection);
        }

        // Smoothly move the character toward the target position
        Vector2 currentPosition = transform.position;
        Vector2 newPosition = Vector2.SmoothDamp(currentPosition, targetPosition, ref velocity, smoothTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    // Calculates the movement input direction
    public Vector2 GetMovementInput()
    {
        Vector2 movementInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) movementInput.y += 1;
        if (Input.GetKey(KeyCode.S)) movementInput.y -= 1;
        if (Input.GetKey(KeyCode.A)) movementInput.x -= 1;
        if (Input.GetKey(KeyCode.D)) movementInput.x += 1;

        return movementInput.normalized;
    }

    // Updates the target position based on input
    public void Move(Vector2 inputDirection)
    {
        // Calculate the next target position
        targetPosition += inputDirection * characterSpeed * Time.deltaTime;
    }

    // Checks if any movement key was released
    public bool CheckKeyRelease()
    {
        return Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
               Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D);
    }
}

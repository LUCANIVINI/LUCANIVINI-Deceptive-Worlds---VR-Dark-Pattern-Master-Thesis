using UnityEngine;

public class TimedMovement : MonoBehaviour
{
    public float targetX; // Target x coordinate
    public float targetZ; // Target z coordinate
    public float moveInterval; // Time in seconds between each movement
    public GameObject PlayerMover; // Secondary object
    public GameObject BlackScreen; // Another object
    public float desiredRotation; // Desired rotation in degrees
    public float blackScreenDuration = 0.1f; // Duration to keep BlackScreen active
    public bool enableBlackScreen; // Tick box to enable or disable BlackScreen functionality

    private void Start()
    {
        // Wait for moveInterval seconds before starting the movement routine
        Invoke(nameof(StartMovementRoutine), moveInterval);
    }

    private void StartMovementRoutine()
    {
        InvokeRepeating(nameof(MoveAndRotate), 0f, moveInterval);
    }

    private void MoveAndRotate()
    {
        // Activate BlackScreen if enabled
        if (enableBlackScreen && BlackScreen != null)
        {
            BlackScreen.SetActive(true);
            Invoke(nameof(DeactivateBlackScreen), blackScreenDuration);
        }
        
        // Get the current rotation of the PlayerMover
        Quaternion currentRotation = PlayerMover.transform.rotation;
        float currentYRotation = currentRotation.eulerAngles.y;

        // Calculate the difference in rotation
        float rotationDifference = desiredRotation - currentYRotation;
        
        // Rotate 'main' by the calculated degrees
        transform.Rotate(0, rotationDifference, 0, Space.World);

        // Calculate the target position based on the absolute position of PlayerMover
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, targetZ);
        Vector3 movementDirection = targetPosition - PlayerMover.transform.position;

        // Move 'main' to the calculated position while keeping its y coordinate unchanged
        transform.position += new Vector3(movementDirection.x, 0, movementDirection.z);
    }

    private void DeactivateBlackScreen()
    {
        // Deactivate BlackScreen if it was enabled
        if (enableBlackScreen && BlackScreen != null)
        {
            BlackScreen.SetActive(false);
        }
    }
}

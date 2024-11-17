using UnityEngine;

public class TimedMovementDisabler : MonoBehaviour
{
    private void Start()
    {
        // Find all objects with the TimedMovement script attached
        TimedMovement[] timedMovements = FindObjectsOfType<TimedMovement>();

        // Disable the TimedMovement script on each found object
        foreach (TimedMovement timedMovement in timedMovements)
        {
            timedMovement.enabled = false;
        }
    }
}

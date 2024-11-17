using UnityEngine;

public class TeleportEnabler : MonoBehaviour
{
    // Public variable to specify the GameObject containing the TimedMovement script
    public GameObject targetObject;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("TeleportEnabler: Target object not specified.");
            return;
        }

        // Try to get the TimedMovement component from the targetObject
        TimedMovement timedMovement = targetObject.GetComponent<TimedMovement>();

        if (timedMovement != null)
        {
            timedMovement.enabled = true; // Enable the TimedMovement script
            Debug.Log("TeleportEnabler: TimedMovement script enabled.");
        }
        else
        {
            Debug.LogError("TeleportEnabler: No TimedMovement script found on the target object.");
        }
    }
}

using UnityEngine;

public class InteractorCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the tag "Apple" or "Grocery"
        if (other.gameObject.tag == "Apple" || other.gameObject.tag == "Grocery")
        {
            // Check if the logging system is initialized before attempting to log
            if (CollisionLogger.Instance != null)
            {
                CollisionLogger.Instance.LogCollision(gameObject, other.gameObject);
            }
            else
            {
                Debug.LogWarning("Logging system not initialized yet.");
            }
        }
    }
}

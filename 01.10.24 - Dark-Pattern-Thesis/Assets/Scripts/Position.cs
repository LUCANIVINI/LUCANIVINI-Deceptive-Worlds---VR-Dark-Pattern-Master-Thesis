using UnityEngine;

public class Position : MonoBehaviour
{
    void Update()
    {
        // Check if the "P" key is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Get the absolute world position of the object
            Vector3 worldPosition = transform.position;

            // Get the relative position of the object (relative to its parent)
            Vector3 localPosition = transform.localPosition;

            // Print the absolute world position
            Debug.Log("World Position (absolute): " + worldPosition);

            // Print the relative position (relative to parent)
            Debug.Log("Local Position (relative to parent): " + localPosition);
        }
    }
}

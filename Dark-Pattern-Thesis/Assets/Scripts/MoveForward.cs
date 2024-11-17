using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [Tooltip("Object to be moved forward.")]
    public Transform objectToMove; // Reference to the object to be moved

    [Tooltip("Distance to move the object forward.")]
    public float distance = 0.5f; // Distance to move the object forward

    public float speed = 5f; // Speed of movement

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to the input you want to use
        {
            if (objectToMove != null)
            {
                // Calculate the forward movement vector based on the object's current rotation
                Vector3 moveDirection = objectToMove.forward;

                // Move the object forward by a certain distance
                objectToMove.position += moveDirection * distance;
            }
            else
            {
                Debug.LogWarning("No object to move assigned!");
            }
        }
    }
}


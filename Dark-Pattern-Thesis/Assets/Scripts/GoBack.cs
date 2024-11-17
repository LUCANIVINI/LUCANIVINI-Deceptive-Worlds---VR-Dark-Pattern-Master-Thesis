using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    // Public fields to define the child objects to be activated/deactivated
    public GameObject childObject1;  // First child object
    public GameObject childObject2;  // Second child object

    // Default coordinates as per your provided screenshot
    public float xMin = 9f;
    public float xMax = 14f;
    public float zMin = -1.2f;
    public float zMax = 4.2f;

    void Update()
    {
        // Get the absolute (world) X and Z position of the object this script is attached to
        float xPosition = transform.position.x;
        float zPosition = transform.position.z;

        // Check if the object is inside the defined X and Z ranges
        if (xPosition >= xMin && xPosition <= xMax && zPosition >= zMin && zPosition <= zMax)
        {
            // If inside the range, disable both child objects
            if (childObject1 != null)
            {
                childObject1.SetActive(false);
            }
            if (childObject2 != null)
            {
                childObject2.SetActive(false);
            }
        }
        else
        {
            // If outside the range, enable both child objects
            if (childObject1 != null)
            {
                childObject1.SetActive(true);
            }
            if (childObject2 != null)
            {
                childObject2.SetActive(true);
            }
        }
    }
}

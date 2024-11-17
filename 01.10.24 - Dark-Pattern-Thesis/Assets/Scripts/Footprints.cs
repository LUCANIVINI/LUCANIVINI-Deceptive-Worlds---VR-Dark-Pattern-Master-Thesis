using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprints : MonoBehaviour
{
    public float enableInterval = 0.5f; // Time between enabling each child
    public float resetDelay = 2.0f; // Time to wait after last child is enabled before resetting

    private GameObject[] childObjects;

    void Start()
    {
        // Get all children and store them in an array
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
            childObjects[i].SetActive(false); // Start with all children disabled
        }

        // Start the coroutine
        StartCoroutine(EnableChildrenSequence());
    }

    IEnumerator EnableChildrenSequence()
    {
        while (true) // Infinite loop to restart the sequence
        {
            // Wait the enable interval before making the first object appear
            yield return new WaitForSeconds(enableInterval);

            // Loop through all children to enable them one by one
            foreach (GameObject child in childObjects)
            {
                child.SetActive(true);
                yield return new WaitForSeconds(enableInterval); // Wait for specified interval
            }

            // After all children are enabled, wait for the reset delay
            yield return new WaitForSeconds(resetDelay);

            // Disable all children
            foreach (GameObject child in childObjects)
            {
                child.SetActive(false);
            }
        }
    }
}

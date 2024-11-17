using UnityEngine;

public class ConditionActivator : MonoBehaviour
{
    // Public array to hold the objects to enable for each condition
    public GameObject[] objectsToEnable;

    void Update()
    {
        // Check for number key presses (1 to 9)
        for (int i = 1; i <= 9; i++)
        {
            // Convert the number to the corresponding KeyCode (e.g., 1 to KeyCode.Alpha1)
            KeyCode key = KeyCode.Alpha0 + i;
            
            // Check if the specific key is pressed
            if (Input.GetKeyDown(key))
            {
                EnableObject(i - 1); // Enable the corresponding object (array is 0-based)
            }
        }
    }

    // Method to enable the object at the specified index
    void EnableObject(int index)
    {
        // Check if the index is within the valid range of the array
        if (index >= 0 && index < objectsToEnable.Length)
        {
            // Check if the object at the specified index is not null
            if (objectsToEnable[index] != null)
            {
                objectsToEnable[index].SetActive(true); // Enable the object
                Debug.Log($"ConditionActivator: Enabled object at index {index + 1}.");
            }
            else
            {
                Debug.LogError($"ConditionActivator: No object assigned at index {index + 1}.");
            }
        }
        else
        {
            Debug.LogError($"ConditionActivator: Invalid index {index + 1}.");
        }
    }
}

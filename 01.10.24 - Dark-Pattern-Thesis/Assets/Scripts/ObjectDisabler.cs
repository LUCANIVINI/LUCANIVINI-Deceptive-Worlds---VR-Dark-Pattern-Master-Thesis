using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    public GameObject objectToDisable; // Public variable to specify the object to disable

    void Start()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // Disable the specified object
            Debug.Log("ObjectDisabler: Object disabled.");
        }
        else
        {
            Debug.LogError("ObjectDisabler: No object assigned to disable.");
        }
    }
}

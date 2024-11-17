using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    public string objectToDisableName; // Name of the object to disable

    void OnEnable()
    {
        // Find and disable the GameObject by name
        GameObject objectToDisable = GameObject.Find(objectToDisableName);
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ObjectControl: No object found with the name " + objectToDisableName);
        }
    }
}

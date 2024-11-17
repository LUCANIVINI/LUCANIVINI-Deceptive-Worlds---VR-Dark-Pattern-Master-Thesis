using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Disabler : MonoBehaviour
{
    public string scriptNameToDisable; // The name of the script to disable

    void Start()
    {
        // Attempt to find the script type using the provided script name
        Type scriptType = Type.GetType(scriptNameToDisable + ", Assembly-CSharp");
        
        if (scriptType == null)
        {
            Debug.LogError("Script type '" + scriptNameToDisable + "' not found. Make sure you have the correct name and it is compiled correctly.");
            return;
        }

        // Find all GameObjects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Iterate over each GameObject and disable the script if it is attached
        foreach (GameObject obj in allObjects)
        {
            Component scriptComponent = obj.GetComponent(scriptType);
            if (scriptComponent != null)
            {
                MonoBehaviour monoBehaviour = scriptComponent as MonoBehaviour;
                if (monoBehaviour != null)
                {
                    monoBehaviour.enabled = false;
                    Debug.Log("Disabled " + scriptNameToDisable + " on " + obj.name);
                }
            }
        }
    }
}

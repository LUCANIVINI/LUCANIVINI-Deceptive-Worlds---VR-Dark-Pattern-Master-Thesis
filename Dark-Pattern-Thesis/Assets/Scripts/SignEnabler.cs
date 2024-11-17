using UnityEngine;

public class SignEnabler : MonoBehaviour
{
    public GameObject signObject; // Public variable to specify the object to enable

    void Start()
    {
        if (signObject != null)
        {
            signObject.SetActive(true); // Enable the sign object
            Debug.Log("SignEnabler: Sign object enabled.");
        }
        else
        {
            Debug.LogError("SignEnabler: No sign object assigned.");
        }
    }
}

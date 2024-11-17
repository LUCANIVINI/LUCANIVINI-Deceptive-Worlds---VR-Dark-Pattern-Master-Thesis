using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignAppear : MonoBehaviour
{
    public GameObject targetObject;  // The object the player should be facing
    public GameObject Sign;  // The main sign object
    public GameObject SignLeft;  // The left sign object
    public GameObject SignRight;  // The right sign object

    public float minAngle = 10f;  // Minimum angle to define left and right ranges

    void Update()
    {
        // Calculate the yaw rotation of the player's forward direction
        Vector3 playerForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;

        // Calculate the direction from the player to the target object on the XZ plane
        Vector3 directionToTarget = new Vector3(targetObject.transform.position.x - transform.position.x, 0, targetObject.transform.position.z - transform.position.z).normalized;

        // Calculate the signed angle between the direction to the target and the player's forward direction
        float angleToTarget = Vector3.SignedAngle(directionToTarget, playerForward, Vector3.up);

        // Right range (+minAngle to +180 degrees)
        if (angleToTarget >= minAngle && angleToTarget <= 180f)
        {
            Sign.SetActive(true);
            SignRight.SetActive(true);
            SignLeft.SetActive(false);
        }
        // Left range (-minAngle to -180 degrees)
        else if (angleToTarget <= -minAngle && angleToTarget >= -180f)
        {
            Sign.SetActive(true);
            SignLeft.SetActive(true);
            SignRight.SetActive(false);
        }
        // Center range: Player is directly facing the target (within -minAngle to +minAngle degrees)
        else
        {
            Sign.SetActive(false);
            SignLeft.SetActive(false);
            SignRight.SetActive(false);
        }
    }
}

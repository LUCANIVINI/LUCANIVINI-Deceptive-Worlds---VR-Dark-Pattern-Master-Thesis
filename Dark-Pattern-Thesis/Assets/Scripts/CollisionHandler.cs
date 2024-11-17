using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactor") && gameObject.CompareTag("Apple"))
        {
            LogManager.Instance.LogCollision(other.gameObject, gameObject);
        }
        else if (other.CompareTag("Apple") && gameObject.CompareTag("Interactor"))
        {
            LogManager.Instance.LogCollision(gameObject, other.gameObject);
        }
    }
}

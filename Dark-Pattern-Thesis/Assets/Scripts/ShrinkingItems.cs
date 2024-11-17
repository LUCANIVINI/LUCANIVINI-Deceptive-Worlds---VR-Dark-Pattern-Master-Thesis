using UnityEngine;

public class ShrinkingItems : MonoBehaviour
{
    [Tooltip("Decrease rate (0.002).")]
    public float decreaseRate = 0.002f; // Decrease rate (0.002)

    private void Start()
    {
        // Randomly wait between 1 and 10 seconds before starting to decrease size
        float randomWaitTime = Random.Range(1f, 3f);
        StartCoroutine(WaitBeforeDecrease(randomWaitTime));
    }

    private System.Collections.IEnumerator WaitBeforeDecrease(float waitTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Start decreasing size
        StartCoroutine(DecreaseSize());
    }

    private System.Collections.IEnumerator DecreaseSize()
    {
        Vector3 originalScale = transform.localScale;

        while (true)
        {
            // Calculate the decrease amount based on the current size
            Vector3 decreaseAmount = originalScale * decreaseRate;

            // Decrease size in all dimensions
            transform.localScale -= decreaseAmount;

            // If any dimension becomes less than or equal to zero, stop the coroutine and deactivate the object
            if (transform.localScale.x <= 0 || transform.localScale.y <= 0 || transform.localScale.z <= 0)
            {
                // Deactivate the object
                gameObject.SetActive(false);
                // Exit the coroutine
                yield break;
            }

            // Update collider size
            UpdateColliderSize();

            // Wait for 0.1 seconds
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void UpdateColliderSize()
    {
        // Get the collider component attached to the object
        Collider collider = GetComponent<Collider>();

        // Check if a collider exists
        if (collider != null)
        {
            // Adjust the collider size to match the object's scale
            collider.transform.localScale = transform.localScale;
        }
    }
}

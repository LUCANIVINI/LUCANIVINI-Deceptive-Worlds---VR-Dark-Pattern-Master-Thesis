using UnityEngine;

public class FadingColors : MonoBehaviour
{
    [Tooltip("Option to deactivate the parent object.")]
    public bool deactivateParent = true; // Option to deactivate the parent object

    [Tooltip("Alpha decrease rate (0.0003).")]
    public float alphaDecreaseRate = 0.0003f; // Alpha decrease rate (0.0003)

    [Tooltip("Color decrease rate (3 times faster).")]
    public float colorDecreaseRate = 3f * 0.0003f; // Color decrease rate (3 times faster)

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        // Get the material of the object this script is attached to
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
        }
        else
        {
            Debug.LogError("No renderer attached to the object!");
            enabled = false; // Disable the script if there's no renderer
        }

        // Set the initial alpha value to 255
        Color color = material.color;
        color.a = 1f;
        material.color = color;

        // Randomly wait between 1 and 10 seconds before starting to affect alpha and color
        float randomWaitTime = Random.Range(1f, 10f);
        StartCoroutine(WaitBeforeFade(randomWaitTime));
    }

    private System.Collections.IEnumerator WaitBeforeFade(float waitTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Start decreasing alpha and converging color to white
        StartCoroutine(FadeToWhite());
    }

    private System.Collections.IEnumerator FadeToWhite()
    {
        Color color = material.color;

        while (color.a > 0)
        {
            // Decrease alpha value
            color.a -= alphaDecreaseRate;
            color.a = Mathf.Clamp01(color.a);

            // Move the color towards white
            color.r += (1 - color.r) * colorDecreaseRate;
            color.g += (1 - color.g) * colorDecreaseRate;
            color.b += (1 - color.b) * colorDecreaseRate;

            // Apply the new color to the material
            material.color = color;

            // Wait for 10 milliseconds
            yield return new WaitForSeconds(0.01f);
        }

        // Deactivate the parent object if specified
        if (deactivateParent && transform.parent != null)
        {
            transform.parent.gameObject.SetActive(false); // Disable the parent
        }
        // No need to deactivate the object explicitly here
    }
}

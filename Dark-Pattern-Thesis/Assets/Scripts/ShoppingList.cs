using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    public string materialsFolderPath = "Materials"; // Path to your folder inside Resources
    public Renderer plane1;  // First plane renderer
    public Renderer plane2;  // Second plane renderer

    void Start()
    {
        // Load all materials from the specified folder in the Resources directory
        Material[] allMaterials = Resources.LoadAll<Material>(materialsFolderPath);

        // Ensure there are enough materials to pick two
        if (allMaterials.Length < 2)
        {
            Debug.LogError("Not enough materials in the folder to pick two!");
            return;
        }

        // Randomly select two different materials
        int firstIndex = Random.Range(0, allMaterials.Length);
        int secondIndex;

        // Ensure the second material is different from the first
        do
        {
            secondIndex = Random.Range(0, allMaterials.Length);
        }
        while (secondIndex == firstIndex);  // This ensures two different materials

        // Assign the first random material to the first plane
        plane1.material = allMaterials[firstIndex];

        // Assign the second random material to the second plane
        plane2.material = allMaterials[secondIndex];
    }
}

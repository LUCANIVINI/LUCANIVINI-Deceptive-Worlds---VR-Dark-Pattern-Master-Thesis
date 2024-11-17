using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CollisionLogger : MonoBehaviour
{
    public static CollisionLogger Instance { get; private set; } // Singleton instance to ensure only one logger exists.
    private StreamWriter writer; // Stream writer to handle file writing.
    private string filePath; // Path to the log file.
    private DateTime startTime; // Time when the logging started.
    private bool fileInitialized = false; // Flag to check if the file has been initialized.
    private bool loggingEnabled = true; // Flag to control logging after a certain event.

    // Variables to keep track of the last collision for debounce logic.
    private string lastInteractorName;
    private string lastAppleName;
    private DateTime lastCollisionTime;

    public GameObject[] conditionObjects; // Array to hold the 9 condition GameObjects

    void Awake()
    {
        // Singleton pattern implementation to avoid duplicate loggers.
        if (Instance != null)
        {
            Destroy(gameObject); // Destroy if an instance already exists.
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on load.
        }
    }

    void Update()
    {
        // Initialize file logging upon first pressing 'A' key.
        if (Input.GetKeyDown(KeyCode.A) && !fileInitialized)
        {
            InitializeFile();
            startTime = DateTime.Now; // Mark the start time of logging.
            fileInitialized = true;
        }
    }

    void InitializeFile()
    {
        // Get the path to the current user's desktop
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        
        // Define the directory and file name for the log, ensuring it uses/creates the folder on the desktop
        string customPath = Path.Combine(desktopPath, "Dark Patterns - CSV_Logs");
        
        if (!Directory.Exists(customPath))
        {
            Directory.CreateDirectory(customPath); // Create directory if it doesn't exist.
        }

        string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string conditionName = "Control Condition"; // Default condition name
        int enabledCount = 0;

        // Check which condition objects are enabled
        for (int i = 0; i < conditionObjects.Length; i++)
        {
            if (conditionObjects[i] != null && conditionObjects[i].activeSelf)
            {
                if (enabledCount == 0)
                {
                    conditionName = "Condition " + (i + 1);
                }
                else
                {
                    conditionName = "ERROR: More than one condition active";
                }
                enabledCount++;
            }
        }

        // Sanitize the filename to remove or replace invalid characters
        string sanitizedConditionName = conditionName.Replace(":", "-").Replace("/", "-").Replace("\\", "-").Replace("?", "").Replace("*", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "");

        // Combine dateTime and condition name to form the file name
        filePath = Path.Combine(customPath, $"{dateTime}_{sanitizedConditionName}.csv");
        writer = new StreamWriter(filePath, true);
        writer.WriteLine("Elapsed Time (min:sec.ms),InteractorName,ObjectName,Tag,BunchName"); // Write the header with the new AppleTag column.
    }

    public void LogCollision(GameObject interactor, GameObject apple)
    {
        // Only log if the file is initialized, writer is ready, and logging hasn't been disabled.
        if (!fileInitialized || writer == null || !loggingEnabled)
            return;

        DateTime now = DateTime.Now;
        // Debounce logic: skip logging if this collision is the same as the last and within 5 seconds.
        if (interactor.name == lastInteractorName && apple.name == lastAppleName &&
            (now - lastCollisionTime).TotalSeconds < 5)
        {
            return;
        }

        // Update last collision details.
        lastInteractorName = interactor.name;
        lastAppleName = apple.name;
        lastCollisionTime = now;

        // Get the parent name of the apple or grocery.
        string bunchName = apple.transform.parent != null ? apple.transform.parent.name : "No Parent";

        // Get the tag of the apple or grocery.
        string appleTag = apple.tag;

        // Calculate elapsed time since logging started.
        TimeSpan elapsed = now - startTime;
        string elapsedTime = $"{elapsed.Minutes}:{elapsed.Seconds}.{elapsed.Milliseconds}";

        // Special case for interactions with CART:
        // If the interactor is "CART" and the object is an "Apple", stop logging.
        if (interactor.name == "CART" && appleTag == "Apple")
        {
            loggingEnabled = false;
        }

        // Log the collision.
        writer.WriteLine($"{elapsedTime},{interactor.name},{apple.name},{appleTag},{bunchName}");
        Debug.Log($"Logged interaction: {interactor.name} with {apple.name} (Tag: {appleTag}, Bunch: {bunchName}) at {elapsedTime}");
    }

    void OnDestroy()
    {
        // Ensure the file is properly closed when the object is destroyed.
        if (writer != null)
        {
            writer.Close();
            writer = null;
            Debug.Log("StreamWriter closed on OnDestroy.");
        }
    }
}

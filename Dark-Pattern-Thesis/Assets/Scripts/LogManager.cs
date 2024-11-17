using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;  // This directive includes the System namespace where DateTime resides

public class LogManager : MonoBehaviour
{
    public static LogManager Instance;
    private StreamWriter writer;
    private string filePath;
    private DateTime startTime;  // DateTime is part of the System namespace
    private bool isLogging = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isLogging)
            {
                StartLogging();
            }
        }
    }

    private void StartLogging()
    {
        string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Dark Patterns Thesis CSV Logs");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        startTime = DateTime.Now;
        filePath = Path.Combine(directoryPath, $"{startTime:yyyy-MM-dd_HH-mm-ss}.csv");
        writer = new StreamWriter(filePath, true);
        writer.WriteLine("Elapsed Time (min:sec.ms),Interactor Name,Apple Name");
        isLogging = true;
    }

    public void LogCollision(GameObject interactor, GameObject apple)
    {
        if (!isLogging) return;
        
        TimeSpan elapsed = DateTime.Now - startTime;
        string elapsedTime = $"{elapsed.Minutes}:{elapsed.Seconds}.{elapsed.Milliseconds}";
        writer.WriteLine($"{elapsedTime},{interactor.name},{apple.name}");
    }

    void OnDestroy()
    {
        if (writer != null)
        {
            writer.Close();
        }
    }
}

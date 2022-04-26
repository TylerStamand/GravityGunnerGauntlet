using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    // Variables that appear in the inspector
    public TextMeshProUGUI timeText;
    public float minutes;
    public float seconds;
    public string formattedTime;

    // Private Variables
    private float timeInSeconds;
    private bool timerIsRunning = false;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;

        // Set the timer to 0
        timeInSeconds = 0;
    }

    void Update()
    {
        // If the times is running
        if (timerIsRunning)
        {
            // Add time
            timeInSeconds += Time.deltaTime;

            // Display the time
            DisplayTime(timeInSeconds);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Convert to minutes, seconds
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Format the time
        formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Put the time in the textbox
        timeText.text = formattedTime;
    }

    public void stopTime()
    {
        // Stop the timer from running
        timerIsRunning = false;
    }

    public void startTime()
    {
        // Start the timer again
        timerIsRunning = true;
    }
}


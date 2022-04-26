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
        timerIsRunning = false;

        // Set the timer to 0
        timeInSeconds = 0;
    }

    void Update()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // If we are on the main menu, reset the time
        if (sceneName == "MainMenu")
            timeInSeconds = 0;

        // Check if we need to start or stop the time
        if (timerIsRunning == false && (sceneName == "Level1" || sceneName == "Level2" || sceneName == "LevelBoss") && GetComponent<Canvas>().enabled == false)
        {
            startTime();
            GetComponent<Canvas>().enabled = true;
        }
        else if(timerIsRunning == true && sceneName == "WinScreen")
        {
            stopTime();
        }
        else if(sceneName == "MainMenu" || sceneName == "TutorialLevel")
        {
            GetComponent<Canvas>().enabled = false;
            stopTime();
        }

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


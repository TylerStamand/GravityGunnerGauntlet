using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int score = 0;
    public int health = 100;
    public bool finalWave = false;

    // Allows us to refer to the game manager as an instance
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    // If there is another game manager destroy it
    private void Awake()
    {
        health = 100;
        score = 0;

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    // Used to change level
    public void goToLevel(int levelNumber)
    {
        if(levelNumber == 1)
        {
            // Turn the cursor on
            Cursor.visible = false;

            // Unlock the cursor from the middle of the screen
            Cursor.lockState = CursorLockMode.Locked;
        }
        GameManager.Instance.addHealth(100);
        SceneManager.LoadScene(levelNumber);
    }

    public void subHealth(int value)
    {
        // Subtract the value from health
        health = health - value;

        if (health <= 0)
        {
            // Turn the cursor on
            Cursor.visible = true;

            // Unlock the cursor from the middle of the screen
            Cursor.lockState = CursorLockMode.None;

            goToLevel(3);
        }
    }

    public void addHealth(int value)
    {
        // Add the value from health
        health = health + value;

        if (health > 100)
        {
            health = 100;
        }
    }

    public void Update()
    {
        //If final wave reached
        if (finalWave)
        {
            int count = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (count == 0)
            {
                // Turn off the check to see if there are enemys in the scene
                // If this is left on (true) it will cause an infinate loop
                // Which keeps re-loading the scene.
                finalWave = false;

                // Turn the cursor on
                Cursor.visible = true;

                // Unlock the cursor from the middle of the screen
                Cursor.lockState = CursorLockMode.None;

                // Move to the victory level
                goToLevel(2);
            }
        }
    }
}

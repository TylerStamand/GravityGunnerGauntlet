using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public int score = 0;
    public int health = 3;

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
        health = 3;
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
        
        GameManager.Instance.addHealth(3);
        SceneManager.LoadScene(levelNumber);
    }

    public void subHealth(int value)
    {
        // Subtract the value from health
        health = health - value;

        if (health <= 0)
        {
            goToLevel(3);
        }
    }

    public void addHealth(int value)
    {
        // Add the value from health
        health = health + value;

        if (health > 3)
        {
            health = 3;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

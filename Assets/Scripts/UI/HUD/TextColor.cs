using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class TextColor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if(sceneName == "WinScreen")
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerUnit playerUnitPrefab;


    private static GameManager _instance;
    

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
      

        SceneManager.LoadScene(levelNumber);

        if (levelNumber == 1)
        {
            playerUnitPrefab.ResetPlayer();
        }

        GameObject playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if(playerSpawn != null) {
            PlayerUnit playerUnit = Instantiate(playerUnitPrefab, playerSpawn.transform.position, Quaternion.identity);

        }
        else {
            Debug.Assert(false, "No player spawn found, not spawning player. Try adding an object with the tag of PlayerSpawn");
        }
        
    }



    public void QuitGame()
    {
        Application.Quit();
    }

}

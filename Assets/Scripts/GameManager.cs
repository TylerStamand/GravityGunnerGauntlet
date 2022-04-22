using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerUnit playerUnitPrefab;


    private static GameManager _instance;
    

    public event Action<PlayerUnit> PlayerSpawn;

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

        SceneManager.sceneLoaded += SetPlayer;

        
        
    }



    public void QuitGame()
    {
        Application.Quit();
    }


    void SetPlayer(Scene scene, LoadSceneMode mode) {
        GameObject playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (playerSpawn != null)
        {
            PlayerUnit playerUnit = Instantiate(playerUnitPrefab, playerSpawn.transform.position, Quaternion.identity);
            PlayerSpawn?.Invoke(playerUnit);
            if (scene.buildIndex == 1)
            {
                playerUnit.ResetPlayer();
                Debug.Log("Reset Player Data from gamescript");
            }
        }
        else
        {
            Debug.Assert(false, "No player spawn found, not spawning player. Try adding an object with the tag of PlayerSpawn");
        }
    }

}

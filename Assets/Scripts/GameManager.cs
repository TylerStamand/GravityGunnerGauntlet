using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerUnit playerUnitPrefab;


    private static GameManager _instance;
    

    public event Action<PlayerUnit> PlayerSpawn;
    
    public bool AllEnemiesKilled;

    bool HUDLoaded;

    // Allows us to refer to the game manager as an instance
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public PlayerUnit PlayerUnit;
    List<Enemy> levelEnemies;

    // If there is another game manager destroy it
    private void Awake()
    {
        HUDLoaded = false;
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
    public void GoToLevel(int levelNumber)
    {
        if(HUDLoaded == true) {
            SceneManager.UnloadSceneAsync("HUD");
            HUDLoaded = false;
        }
        
        SceneManager.LoadScene(levelNumber);
        //IF LEVEL
        if(levelNumber > 0 || levelNumber < 5) {
            
            SceneManager.sceneLoaded += SetPlayer;
            SceneManager.sceneLoaded += SetEnemies;
        }



        
        
    }



    public void QuitGame()
    {
        Application.Quit();
    }


    void SetPlayer(Scene scene, LoadSceneMode mode) {
        if(PlayerUnit != null) {
            PlayerUnit.OnDead -= HandlePlayerDeath;
        }

        GameObject playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (playerSpawn != null)
        {
            PlayerUnit = Instantiate(playerUnitPrefab, playerSpawn.transform.position, Quaternion.identity);
            PlayerUnit.OnDead += HandlePlayerDeath;

            PlayerSpawn?.Invoke(PlayerUnit);
            if (scene.buildIndex == 1)
            {
                PlayerUnit.ResetPlayer();
                Debug.Log("Reset Player Data from gamescript");
            }

        }
        
        else
        {
            Debug.Assert(false, "No player spawn found, not spawning player. Try adding an object with the tag of PlayerSpawn");
        }
        SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
        HUDLoaded = true;
        SceneManager.sceneLoaded -= SetPlayer;
    }


    void SetEnemies(Scene scene, LoadSceneMode mode) {
        Debug.Log("Enemies Set");
        levelEnemies = FindObjectsOfType<Enemy>().ToList<Enemy>();
        Debug.Log("Enemy count: " + levelEnemies.Count);
        foreach(Enemy enemy in levelEnemies) {
            enemy.OnDeath.AddListener(RemoveEnemyFromList);
        }
        if(levelEnemies.Count > 0) {
            AllEnemiesKilled = false;
        }
        else AllEnemiesKilled = true;
        SceneManager.sceneLoaded -= SetEnemies;
        
    }
    void RemoveEnemyFromList(Enemy enemy) {
        
        levelEnemies.Remove(enemy);
        Debug.Log("Enemies left: " + levelEnemies.Count);
        if(levelEnemies.Count == 0) {
            AllEnemiesKilled = true;
        }
    }

    void HandlePlayerDeath() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Show death scene, add timer then restart level
        GoToLevel(sceneIndex);
    }

}

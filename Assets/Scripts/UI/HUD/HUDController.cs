using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Hearts")]
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject heartParent;

    [Header("Grav Watch")]
    [SerializeField] Image gravWatch;
    [SerializeField] Color gravNotReadyColor;

    [Header("Enemy Counter")]
    [SerializeField] TextMeshProUGUI enemiesText;

    List<GameObject> hearts;

    void Awake()
    {
        GameManager.Instance.PlayerUnit.OnHealthChange += UpdateHearts;
        GameManager.Instance.PlayerUnit.OnGravStatusChange += UpdateGrav;
        GameManager.Instance.PlayerUnit.OnGravEnabled += EnableGravIcon;
        GameManager.Instance.EnemyCountChange += UpdateEnemyCount;
        hearts = new List<GameObject>();

        UpdateEnemyCount(GameManager.Instance.EnemyCount);
        UpdateHearts(GameManager.Instance.PlayerUnit.CurrentHealth);
        UpdateGrav(true);
        EnableGravIcon(GameManager.Instance.PlayerUnit.GravityEnabled);

        if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3) {
            enemiesText.enabled = true;
        }
        else {
            enemiesText.enabled = false;
        }
    }

    void UpdateHearts(int health)
    {
        foreach(GameObject heart in hearts) {
            Destroy(heart);
        }
        for(int i = 0; i < health; i++) {
            GameObject heart = Instantiate(heartPrefab, Vector3.zero, Quaternion.identity);
            heart.transform.SetParent(heartParent.transform);
            hearts.Add(heart);
        }
    }

    void EnableGravIcon(bool enable) {
        gravWatch.enabled = enable;
    }

    void UpdateGrav(bool ready) {
        if(ready) {
            gravWatch.color = Color.white;
        
        }
        else {
            gravWatch.color = gravNotReadyColor;
        }
    }

    void UpdateEnemyCount(int enemies) {
        enemiesText.text = "Enemies: " + enemies;
    }


}

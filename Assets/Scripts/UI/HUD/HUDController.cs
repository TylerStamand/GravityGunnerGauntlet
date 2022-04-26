using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject heartParent;

    [Header("Grav Watch")]
    [SerializeField] Image gravWatch;
    [SerializeField] Color gravNotReadyColor;

    List<GameObject> hearts;

    void Awake()
    {
        GameManager.Instance.PlayerUnit.OnHealthChange += UpdateHearts;
        GameManager.Instance.PlayerUnit.OnGravStatusChange += UpdateGrav;
        GameManager.Instance.PlayerUnit.OnGravEnabled += EnableGravIcon;
        hearts = new List<GameObject>();
        UpdateHearts(GameManager.Instance.PlayerUnit.CurrentHealth);
        UpdateGrav(true);
        EnableGravIcon(GameManager.Instance.PlayerUnit.GravityEnabled);
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


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject heartParent;

    List<GameObject> hearts;

    void Awake()
    {
        GameManager.Instance.PlayerUnit.OnHealthChange += UpdateHearts;
        hearts = new List<GameObject>();
        UpdateHearts(GameManager.Instance.PlayerUnit.CurrentHealth);
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


}

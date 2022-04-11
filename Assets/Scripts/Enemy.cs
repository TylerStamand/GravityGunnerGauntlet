using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        //This is where you would change a health stat or do any logic after taking a hit.
        //Destroy is just a placeholder to show it works
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;

    int currentHealth;

    void Awake() {
        currentHealth = maxHealth;
    }



    public void TakeDamage(int damage) {
        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

}

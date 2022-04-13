using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;

    protected SpriteRenderer spriteRenderer;

    protected int currentHealth;

    protected virtual void Awake() {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    public void TakeDamage(int damage) {
        if(currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

}

using System;
using UnityEngine;

public class PlayerUnit : MonoBehaviour, IDamageable {

    [SerializeField] int maxHealth = 3;
    [SerializeField] Weapon weapon;
    [SerializeField] PlayerMovement playerMovement;

    public event Action<int> OnHealthChange;

    int currentHealth;

    void Awake() {
        ChangeHealth(maxHealth);

        playerMovement.OnFireEvent += Shoot;
    }

    public void TakeDamage(int damage) {
        ChangeHealth(currentHealth--);
        if(currentHealth <= 0) {
            //Change state to dead
            Debug.Log("Dead");
        }
    }

    void ChangeHealth(int newHealth) {
        currentHealth = newHealth;
        OnHealthChange?.Invoke(currentHealth);
    }

    void Shoot() {
        weapon.Shoot();
    }

}

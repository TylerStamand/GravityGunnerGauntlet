using System;
using UnityEngine;

public class PlayerUnit : MonoBehaviour, IDamageable {

    [SerializeField] int maxHealth = 3;
    [SerializeField] Weapon weapon;
    

    public event Action<int> OnHealthChange;

    PlayerMovement playerMovement;

    int currentHealth;

    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        if(playerMovement != null) {
            playerMovement.OnFireEvent += Shoot;
        }


        ChangeHealth(maxHealth);
        
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

using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerUnit : MonoBehaviour, IDamageable {

    [SerializeField] PlayerData playerData;

    [SerializeField] int maxHealth = 3;
    [SerializeField] Weapon weapon;
    
    [SerializeField] SpriteLibraryAsset noBoots;
    [SerializeField] SpriteLibraryAsset boots;
    [SerializeField] SpriteLibraryAsset bootsAndGrav;


    public event Action<int> OnHealthChange;

    PlayerMovement playerMovement;
    SpriteLibrary spriteLibrary;
    

    int currentHealth;

    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        spriteLibrary = GetComponent<SpriteLibrary>();

        Initialize();
        
    }

    public void TakeDamage(int damage) {
        ChangeHealth(currentHealth - 1);

        Debug.Log("Damage taken " + damage + " , now at " + currentHealth + " health");

        if(currentHealth <= 0) {
            //Change state to dead
            Debug.Log("Dead");
        }
    }


    public void ResetPlayer() {
        playerData.ResetPlayerData();
        Initialize();
    }

    public void EnableBoots() {
        playerMovement.BootsEnabled = true;
        spriteLibrary.spriteLibraryAsset = boots;

    }

    public void EnableGravity() {
        playerMovement.GravityEnabled = true;
        spriteLibrary.spriteLibraryAsset = bootsAndGrav;
    }

    void Initialize() {
        //Set Player Movement script up
        if (playerMovement != null)
        {
            playerMovement.OnFireEvent += Shoot;
            playerMovement.BootsEnabled = playerData.BootsEnabled;
            playerMovement.GravityEnabled = playerData.GravityEnabled;
        }

        //Sets sprite based on abilities unlocked
        if (playerData.BootsEnabled && playerData.GravityEnabled)
        {
            spriteLibrary.spriteLibraryAsset = bootsAndGrav;
        }
        else if (playerData.BootsEnabled)
        {
            spriteLibrary.spriteLibraryAsset = boots;
        }
        else
        {
            spriteLibrary.spriteLibraryAsset = noBoots;
        }

        ChangeHealth(maxHealth);
        
    }

    void ChangeHealth(int newHealth)
    {
        if (newHealth < 0)
        {
            newHealth = 0;
        }
        currentHealth = newHealth;
        OnHealthChange?.Invoke(currentHealth);
    }

    void Shoot()
    {
        weapon.Shoot();
    }

}

using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerUnit : MonoBehaviour, IDamageable {

    [SerializeField] PlayerData playerData;

    [SerializeField] int maxHealth = 3;
    [SerializeField] float damageCoolDown = 1;

    [SerializeField] Weapon weapon;
    
    [SerializeField] SpriteLibraryAsset noBoots;
    [SerializeField] SpriteLibraryAsset boots;
    [SerializeField] SpriteLibraryAsset bootsAndGrav;

    public int CurrentHealth {get; private set;} 

    public event Action OnDead;
    public event Action<int> OnHealthChange;
    public event Action<bool> OnGravStatusChange;

    PlayerMovement playerMovement;
    SpriteLibrary spriteLibrary;
    Animator animator;
    
    float timeSinceLastDamage;
    
    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        spriteLibrary = GetComponent<SpriteLibrary>();
        animator = GetComponent<Animator>();
        timeSinceLastDamage = float.MaxValue;
        
        Initialize();
        
    }

    void Update() {
        timeSinceLastDamage += Time.deltaTime;
    }

    public void TakeDamage(int damage, Vector3 knockbackDirection) {
        if(timeSinceLastDamage >= damageCoolDown) {
            animator.SetTrigger("hit");
            playerMovement.ApplyKnockBack(knockbackDirection);
            ChangeHealth(CurrentHealth - damage);
            

            Debug.Log("Damage taken " + damage + " , now at " + CurrentHealth + " health");

            if (CurrentHealth <= 0)
            {
                //Change state to dead
                Debug.Log("Dead");
                animator.SetBool("dead", true);
                playerMovement.enabled = false;
                OnDead?.Invoke();
            }

            timeSinceLastDamage = 0;
        }
      
    }


    public void ResetPlayer() {
        playerData.ResetPlayerData();
        Initialize();
    }

    public void EnableBoots() {
        playerMovement.BootsEnabled = true;
        spriteLibrary.spriteLibraryAsset = boots;
        playerData.BootsEnabled = true;

    }

    public void EnableGravity() {
        playerMovement.GravityEnabled = true;
        spriteLibrary.spriteLibraryAsset = bootsAndGrav;
        playerData.GravityEnabled = true;
    }

    public Direction GetGravityState() {
        return playerMovement.gravityState;
    }

    void Initialize() {
        //Set Player Movement script up
        if (playerMovement != null)
        {
            playerMovement.OnFireEvent += Shoot;
            playerMovement.OnGravStatusChange += (ready) => OnGravStatusChange?.Invoke(ready);
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
        CurrentHealth = newHealth;
        OnHealthChange?.Invoke(CurrentHealth);
    }

    void Shoot()
    {
        weapon.Shoot();
    }



}

using System.Collections;
using UnityEngine;


public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected int currentHealth;
    protected bool dead;

    protected virtual void Awake() {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update() {
        
    }

    public void TakeDamage(int damage) {
        currentHealth--;
        if(currentHealth <= 0) {
            StartCoroutine(Die());
        }
        else {
            if(animator != null) {
                animator.SetTrigger("hit");
            }
            
        
            
        }
    }

    IEnumerator Die() {
        dead = true;

        if(animator != null) {
            animator.SetBool("dead", true);
        }   
        
     
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}

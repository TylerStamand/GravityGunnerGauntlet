using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth;

    public UnityEvent<Enemy> OnDeath;

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

    public virtual void TakeDamage(int damage, Vector3 knockbackVector) {
        if(!dead) {
            currentHealth--;
            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                if (animator != null)
                {
                    animator.SetTrigger("hit");
                }
            }
        }
       
    }

    IEnumerator Die() {
        if(!dead) {
            dead = true;
            OnDeath?.Invoke(this);
            if (animator != null)
            {
                animator.SetBool("dead", true);
            }


            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
        
    }

}

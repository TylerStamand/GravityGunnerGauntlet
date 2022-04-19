using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{

    [SerializeField] int damage = 1;
    [SerializeField] float moveSpeed = 1;

    GameObject parentTransform;
    Animator animator;
    bool hitGround = false;
    new Collider2D collider;
    new Rigidbody2D rigidbody;

    void Awake() {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity =  transform.up * moveSpeed;
    }

    void FixedUpdate() {
        if(hitGround) {
            rigidbody.velocity = Vector2.zero;
        }
      
    }

    void OnCollisionEnter2D(Collision2D collision) {
        //check if object has IDamageable
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null && parentTransform != collision.gameObject) {
            damageable.TakeDamage(damage);
            Debug.Log(collision.gameObject.name + " took damage");
            Destroy(gameObject);
        }
        else if(parentTransform != collision.gameObject) {
            Debug.Log(collision.gameObject.name);
            collider.enabled = false;
            hitGround = true;
            animator.SetBool("groundHit", hitGround);
        
        }
       
    }

    public void SetParentTransform(GameObject parentTransform) {
        this.parentTransform = parentTransform;    
    }

    public void Kill() {
        
        Destroy(gameObject);
    }


}

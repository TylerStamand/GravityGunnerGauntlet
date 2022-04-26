using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour {

    [SerializeField] int damage = 1;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] AudioPlayer audioPlayer;

    Animator animator;
    bool hitGround = false;
    new Collider2D collider;
    new Rigidbody2D rigidbody;

    void Awake() {
        if(audioPlayer != null) {
            Instantiate(audioPlayer, transform.position, Quaternion.identity);
        }
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.velocity =  transform.up * moveSpeed;
    }


    void OnCollisionEnter2D(Collision2D collision) {
        //check if object has IDamageable
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null) {
            damageable.TakeDamage(damage, transform.position - collision.transform.position);
            rigidbody.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        else {
            this.collider.enabled = false;
            hitGround = true;

            if(animator != null) {
                animator.SetBool("groundHit", hitGround);
            }
            else {
                
                Destroy(gameObject);
            }
            rigidbody.velocity = Vector2.zero;
        
        }
       
    }


    public void Kill() {
        
        Destroy(gameObject);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{

    [SerializeField] int damage = 1;
    [SerializeField] float moveSpeed = 1;

    GameObject parentTransform;

    void Update() {

        Vector3 newPos = transform.up * moveSpeed * Time.deltaTime + transform.position;
        transform.position = newPos;
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
            Destroy(gameObject);
        }
       
    }

    public void SetParentTransform(GameObject parentTransform) {
        this.parentTransform = parentTransform;    
    }


}

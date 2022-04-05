using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{

    [SerializeField] int damage = 1;
    [SerializeField] float moveSpeed = 1;

    void Update() {
       Vector3 newPos =  transform.rotation.eulerAngles.normalized * moveSpeed + transform.position;
       transform.position = newPos;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        //check if object has IDamageable
        Debug.Log("Collided with" + collision.gameObject.GetType());
        Destroy(gameObject);
    }


}

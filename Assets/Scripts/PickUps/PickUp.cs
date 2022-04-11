using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickUp : MonoBehaviour {

    [SerializeField] float rotationSpeed = 20;
    [SerializeField] float floatSpeed;
    [SerializeField] float deltaY;

    float startY;
    bool moveUp; 
    void Awake() {
        GetComponent<Collider2D>().isTrigger = true;
        startY = transform.position.y;
        moveUp = true;
    }
    void Update() {
        AnimationUpdate();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        PlayerUnit playerUnit = collider.gameObject.GetComponent<PlayerUnit>();
        if(playerUnit != null) {
            PickUpAction(playerUnit);
            Destroy(gameObject);
        }
    }

    protected abstract void PickUpAction(PlayerUnit playerUnit);

    void AnimationUpdate() {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        if (transform.position.y < startY - deltaY) {
            moveUp = true;
        }
        if (transform.position.y > startY + deltaY) {
            moveUp = false;
        }

        if (moveUp) {
            transform.position = new Vector3(transform.position.x, transform.position.y + floatSpeed * Time.deltaTime, transform.position.z);
        }
        else {
            transform.position = new Vector3(transform.position.x, transform.position.y - floatSpeed * Time.deltaTime, transform.position.z);
        }
    }
}

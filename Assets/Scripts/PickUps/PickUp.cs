using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickUp : MonoBehaviour
{
   
    void Update() {
        //Do a spinning animation or something
    }

    void OnTriggerEnter2D(Collider2D collider) {
        PlayerUnit playerUnit = collider.gameObject.GetComponent<PlayerUnit>();
        if(playerUnit != null) {
            PickUpAction(playerUnit);
        }
        Destroy(gameObject);
    }

    protected abstract void PickUpAction(PlayerUnit playerUnit);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PlayerUnit playerUnit;
    new Rigidbody2D rigidbody2D;

    void Awake() {
        GameManager.Instance.PlayerSpawn += SetPlayer;
        playerUnit = FindObjectOfType<PlayerUnit>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if(playerUnit != null) {
            SetPlayer(playerUnit);
        }

    }

    void FixedUpdate() {
        if(playerUnit != null && rigidbody2D != null) {
            Vector3 playerPosition = playerUnit.transform.position;
            rigidbody2D.MovePosition(new Vector3(playerPosition.x, playerPosition.y, transform.position.z));
        }

    }

    void SetPlayer(PlayerUnit playerUnit) {
        this.playerUnit = playerUnit;
    }
}

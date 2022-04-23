using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PlayerUnit playerUnit;

    void Awake() {
        GameManager.Instance.PlayerSpawn += SetPlayer;
        playerUnit = FindObjectOfType<PlayerUnit>();
        if(playerUnit != null) {
            SetPlayer(playerUnit);
        }
    }

    void Update() {
        if(playerUnit != null) {
            Vector3 playerPosition = playerUnit.transform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
        }

    }

    void SetPlayer(PlayerUnit playerUnit) {
        this.playerUnit = playerUnit;
    }
}

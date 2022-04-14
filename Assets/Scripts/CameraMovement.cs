using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PlayerUnit playerUnit;

    void Awake() {
        playerUnit = FindObjectOfType<PlayerUnit>();
    }

    void Update() {
        Vector3 playerPosition = playerUnit.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, transform.position.z);
    }
}

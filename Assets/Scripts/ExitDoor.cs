using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] bool openOnEnemiesKilled = true;
    [SerializeField] int levelToSwitchTo;


    bool canOpen;

    void Update() {
        if(openOnEnemiesKilled && GameManager.Instance.AllEnemiesKilled) {
            canOpen = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(canOpen) {
            GameManager.Instance.GoToLevel(levelToSwitchTo);
        }
    }


    //Used for other activations other than enemies killed requirement
    public void SetDoorCanOpen() {
        canOpen = true;
    }

    
}

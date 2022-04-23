using UnityEngine;

public class BossWall : MonoBehaviour {
    static float electicityDuration = 10;
    public Direction Side;


    bool electicityOn;

    float timeElectricityOn;

    void Awake() {
        electicityOn = false;
    }

    void Update() {
        if(electicityOn) {
            timeElectricityOn += Time.deltaTime;
            if(timeElectricityOn >= electicityDuration) {
                electicityOn = false;
            }
        }
    }
 
    void OnTriggerEnter2D(Collider2D collider) {
       
        if(electicityOn) {
            PlayerUnit playerUnit = collider.gameObject.GetComponent<PlayerUnit>();
            if (playerUnit!= null) {
                playerUnit.TakeDamage(1, playerUnit.transform.up);
            }
        }

        
    }

    public void ActivateElectricity() {
        electicityOn = true;
        timeElectricityOn = 0;
    }
}

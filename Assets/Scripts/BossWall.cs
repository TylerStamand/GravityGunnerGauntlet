using UnityEngine;

public class BossWall : MonoBehaviour {

    [SerializeField] ParticleSystem electricityParticles;

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
                electricityParticles.Stop();
            }
        }
    }
 
    void OnTriggerStay2D(Collider2D collider) {
       
        if(electicityOn) {
            PlayerUnit playerUnit = collider.gameObject.GetComponent<PlayerUnit>();
            if (playerUnit!= null) {
                playerUnit.TakeDamage(1, playerUnit.transform.up);
            }
        }

        
    }

    public void ActivateElectricity() {
        electicityOn = true;
        electricityParticles.Play();
        timeElectricityOn = 0;
    }
}

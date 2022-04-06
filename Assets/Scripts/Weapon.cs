using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] Projectile projectile;
    [SerializeField] float coolDown;

    float timeSinceLastShot;

    void Awake() {
        timeSinceLastShot = float.MaxValue;

    }

    void Update() {
        coolDown += Time.deltaTime;
    }

    public void Shoot() {
        if(timeSinceLastShot >= coolDown) {
            
            Projectile shot = Instantiate(projectile, transform.position, transform.rotation);
        
        }
        coolDown = 0;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    [SerializeField] Weapon weapon;
    [SerializeField] float attackCoolDown = 2;
    [SerializeField] float rangeRadius = 1;
    [SerializeField] LayerMask playerLayer;

    float timeSinceLastShoot;

    protected override void Awake()
    {
        base.Awake();
        timeSinceLastShoot = float.MaxValue;
    }



    protected override void Update()
    {
        base.Update();


        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
        PlayerUnit player = collider2D?.gameObject.GetComponent<PlayerUnit>();

    
        if(timeSinceLastShoot >= attackCoolDown && player != null) {
            weapon.transform.up = player.transform.position - weapon.transform.position;
            weapon.Shoot();
            timeSinceLastShoot = 0;
        }
        

        timeSinceLastShoot += Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Enemy
{
    [SerializeField] Weapon weapon;
    [SerializeField] float attackCoolDown = 2;
    [SerializeField] float rangeRadius = 1;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int meleeDamage = 1;

    float timeSinceLastShoot;

    protected override void Awake()
    {
        base.Awake();
        timeSinceLastShoot = float.MaxValue;
    }



    protected override void Update()
    {
        base.Update();

        if(!dead) {
            Collider2D collider2D = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
            PlayerUnit player = collider2D?.gameObject.GetComponent<PlayerUnit>();


            if (timeSinceLastShoot >= attackCoolDown && player != null)
            {
                weapon.transform.up = player.transform.position - weapon.transform.position;
                animator.SetBool("attacking", true);
                weapon.Shoot();
                animator.SetBool("attacking", false);
                timeSinceLastShoot = 0;
            }

        }
       

        timeSinceLastShoot += Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

        if (player != null)
        {
            player.TakeDamage(meleeDamage, transform.position - collision.transform.position);
        }
    }
}

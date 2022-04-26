using DG.Tweening;
using UnityEngine;

public class Flying : Enemy
{

    [SerializeField] float rangeRadius = 1;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float attackCoolDown = 2;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int damage = 1;
    float timeSinceLastAttack;

    protected override void Awake() {
        base.Awake();
        timeSinceLastAttack = float.MaxValue;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
    
        PlayerUnit player = collider2D?.gameObject.GetComponent<PlayerUnit>();

        if(dead) {
            transform.DOKill();
        }

        else {
            if (player != null && timeSinceLastAttack > attackCoolDown)
            {
                animator.SetBool("attacking", true);

                timeSinceLastAttack = 0;
                transform.DOMove(player.transform.position, moveSpeed, false).SetSpeedBased(true).SetEase(Ease.InOutBack).onComplete +=
                    () => animator.SetBool("attacking", false);
            }
        }


       
        
        timeSinceLastAttack += Time.deltaTime;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!dead) {
            PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

            if (player != null)
            {
                player.TakeDamage(damage, transform.position - collision.transform.position);
            }
        }
        
    }
}

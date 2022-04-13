using DG.Tweening;
using UnityEngine;

public class Flying : MonoBehaviour
{

    [SerializeField] float rangeRadius = 1;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float attackCoolDown = 2;
    [SerializeField] LayerMask playerLayer;

    float timeSinceLastAttack;

    void Awake() {
        timeSinceLastAttack = float.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
        
        PlayerUnit player = collider2D?.gameObject.GetComponent<PlayerUnit>();
        if(player != null && timeSinceLastAttack > attackCoolDown) {
            Debug.Log("Attack");
            timeSinceLastAttack = 0;
            transform.DOMove(player.transform.position, moveSpeed, false).SetSpeedBased(true);
        }
        timeSinceLastAttack += Time.deltaTime;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, rangeRadius);
    }
}

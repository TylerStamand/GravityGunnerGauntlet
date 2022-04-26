using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Melee : Enemy {

    [SerializeField] float moveSpeed = 3;
    [SerializeField] float moveRadius = 2;
    
    [Header("Jumping")]
    [SerializeField] bool canJump;
    [SerializeField] float jumpCoolDown;
    [SerializeField] float jumpForce;

    [Header("Combat")]
    [SerializeField] int damage = 1;

    Rigidbody2D rigidBody;

    Vector3 startPos = Vector3.zero;

    bool spriteFliped;
    float timeSinceLastJump;


    

    protected override void Awake() {
        base.Awake();
        rigidBody = GetComponent<Rigidbody2D>();
        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
        startPos = transform.position;
        spriteFliped = !spriteRenderer.flipX;

       
        if(moveRadius > 0) {
            transform.position = transform.position + transform.right * moveRadius;
            transform.DOMove(transform.position - transform.right * 2 * moveRadius, moveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(() => { spriteFliped = !spriteFliped; spriteRenderer.flipX = spriteFliped; });

        }
        
    //    if(canJump) {
    //         GameObject parent = new GameObject("parent");
    //         parent.transform.position = transform.position;
    //         parent.transform.rotation = transform.rotation;
    //         transform.SetParent(parent.transform); 
    //    }
    

    }
    
    protected override void Update() {
        if(dead) {
            transform.DOKill();
        }
        // if(!dead && canJump && timeSinceLastJump > jumpCoolDown) {
            
        //     transform.parent.DOMove(transform.position + transform.parent.up * jumpForce, .3f).SetLoops(1, LoopType.Yoyo);
        //     timeSinceLastJump = 0;
        // }
        // timeSinceLastJump += Time.deltaTime;
    }

    void FixedUpdate() {
        //Apply Gravity
        if(!dead) {
            rigidBody.AddForce(transform.up * Physics.gravity.y * rigidBody.mass);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(!dead) {
            PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

            if (player != null)
            {
                player.TakeDamage(damage, transform.position - collision.transform.position);
            }
        }
        
    }

    void OnDrawGizmosSelected() {
        Vector3 leftBound;
        Vector3 rightBound;
        Gizmos.color = Color.red;
        if(startPos == Vector3.zero) {
            leftBound = transform.position - transform.right * moveRadius;
            rightBound = transform.position + transform.right * moveRadius;
        }
        else {
            leftBound = startPos - transform.right * moveRadius;
            rightBound = startPos + transform.right * moveRadius;
        }
   
        Gizmos.DrawLine(leftBound, rightBound);
    }

   

   



}

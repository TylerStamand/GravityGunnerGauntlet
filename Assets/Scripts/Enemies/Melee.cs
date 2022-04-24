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

    bool moveRight;
    bool spriteFliped;
    float timeSinceLastJump;


    Sequence tweens;

    protected override void Awake() {
        base.Awake();
        rigidBody = GetComponent<Rigidbody2D>();
        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
        startPos = transform.position;
        moveRight = false;
        spriteFliped = !spriteRenderer.flipX;

        transform.position = transform.position + transform.right * moveRadius;

        tweens.Append(transform.DOMove(transform.position - transform.right * 2 * moveRadius, moveSpeed)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete( () => {spriteFliped = !spriteFliped; spriteRenderer.flipX = spriteFliped;}  ));

       
    

    }
    
    protected override void Update() {
        if(dead) {
            rigidBody.DOKill();
        }
        // if(!dead && canJump && timeSinceLastJump > jumpCoolDown) {
            
        //     tweens.Join(transform.DOMove(transform.position + transform.up * jumpForce, .3f));
        //     timeSinceLastJump = 0;
        // }
        timeSinceLastJump += Time.deltaTime;
    }

    void FixedUpdate() {
        //Apply Gravity
        if(!dead) {
            rigidBody.AddForce(transform.up * Physics.gravity.y * rigidBody.mass);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

        if(player != null) {
            player.TakeDamage(damage, transform.position - collision.transform.position );
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

using UnityEngine;

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

    protected override void Awake() {
        base.Awake();
        rigidBody = GetComponent<Rigidbody2D>();
        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
        startPos = transform.position;
        moveRight = true;
        spriteFliped = spriteRenderer.flipX;

    }
    
    protected override void Update() {
        if(!dead) {
            Move();
        }
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
            player.TakeDamage(damage);
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

    void Move() {
        //For moving left and right
        Vector3 rightMovement = Vector3.Scale(transform.right, transform.position);
        Vector3 initalPoint = Vector3.Scale(transform.right, startPos);


        if (Vector3.Distance(rightMovement, initalPoint) >= Vector3.Distance(initalPoint, initalPoint + transform.right * moveRadius))
        {
            if(moveRight == false) {
                moveRight = true;
                spriteFliped = !spriteFliped;
                spriteRenderer.flipX = spriteFliped;
                
            }
            else {
                moveRight = false;
                spriteFliped = !spriteFliped;
                spriteRenderer.flipX = spriteFliped;
            }
            
        }

        if (moveRight)
        {
            transform.position = transform.position + transform.right * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position - transform.right * moveSpeed * Time.deltaTime;
        }

        //For jumping (if enabled)
        if(canJump) {
            if(timeSinceLastJump >= jumpCoolDown ) {
                rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                timeSinceLastJump = 0;
            }
        }

        
    }



}

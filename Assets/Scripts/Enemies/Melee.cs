using UnityEngine;

public class Melee : Enemy {

    [SerializeField] float moveSpeed = 3;
    [SerializeField] float moveRadius = 2;
    
    [Header("Jumping")]
    [SerializeField] bool canJump;
    [SerializeField] float jumpCoolDown;
    [SerializeField] float jumpForce;

    Rigidbody2D rigidBody;

    Vector3 startPos = Vector3.zero;
    bool moveRight;
    float timeSinceLastJump;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
        startPos = transform.position;
        moveRight = true;

    }
    
    void Update() {
        timeSinceLastJump += Time.deltaTime;
        Move();
    }

    void FixedUpdate() {
        //Apply Gravity
        rigidBody.AddForce(transform.up * Physics.gravity.y * rigidBody.mass);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

        if(player != null) {
            player.TakeDamage(1);
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


        // //For flipping sprite based on direction
        // #region 
        // if (gravityState == GravityState.Right || gravityState == GravityState.Down)
        // {
        //     if (value > 0)
        //     {
        //         spriteRenderer.flipX = false;
        //     }
        //     else if (value < 0)
        //     {
        //         spriteRenderer.flipX = true;
        //     }
        // }
        // else
        // {
        //     if (value > 0)
        //     {
        //         spriteRenderer.flipX = true;
        //     }
        //     else if (value < 0)
        //     {
        //         spriteRenderer.flipX = false;
        //     }
        // }

        // #endregion


        float startX = startPos.x;

        //For moving left and right
        float rightMovement = Vector3.Scale(transform.right, transform.position).magnitude;
        float initalPoint = Vector3.Scale(transform.right, startPos).magnitude;

        Debug.Log(rightMovement-initalPoint + " Max " + (initalPoint + moveRadius) + (rightMovement-initalPoint >= (initalPoint + moveRadius)) + " " + moveRight);
        
        
        if (rightMovement-initalPoint >= (initalPoint + moveRadius))
        {
            if(moveRight == false) {
                moveRight = true;
            }
            else {
                moveRight = false;
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

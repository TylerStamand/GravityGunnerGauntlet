using UnityEngine;

public class Melee : Enemy {

    [SerializeField] float moveSpeed = 3;
    [SerializeField] float moveRadius = 2;

    [Tooltip("This can't be changed after starting the level")]
    [SerializeField] GravityState gravityState;
    
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

        SetGravity();
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
        if (transform.position.x < startX - moveRadius)
        {
            moveRight = true;
        }
        if (transform.position.x > startX + moveRadius)
        {
            moveRight = false;
        }

        if (moveRight)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }

        //For jumping (if enabled)
        if(canJump) {
            if(timeSinceLastJump >= jumpCoolDown ) {
                rigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                timeSinceLastJump = 0;
            }
        }

        
    }

    void SetGravity() {
        switch (gravityState)
        {
            case GravityState.Down:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case GravityState.Left:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case GravityState.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case GravityState.Up:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }

}

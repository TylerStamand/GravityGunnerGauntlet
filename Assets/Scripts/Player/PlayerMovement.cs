using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float groundBoost = 5;
    [SerializeField] float shootBoost = 2;
    [SerializeField] float knockbackForce = 1;

    [SerializeField] float jumpCoolDown = .3f;
    [SerializeField] float gravCoolDown = 1f;
    
    [SerializeField] Collider2D groundCollider;
    [SerializeField] LayerMask terrainLayer;

    public event Action OnFireEvent;
    public event Action<bool> OnGravStatusChange;

    public Direction gravityState {get; private set;}
    public bool GravityEnabled {get; set;}
    public bool BootsEnabled {get; set;}

    Animator animator;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    PlayerControls playerControls;


    bool isGrounded;
    bool gravReady;
    float timeSinceLastJump;
    float timeSinceLastGrav;
    

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;

        //set initial gravity to down
        ChangeGravity(Direction.Down);

        timeSinceLastJump = float.MaxValue;
        gravReady = true;
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void Start() {
        playerControls.Player.SwitchGravity.performed += OnSwitchGravity;
        playerControls.Player.Fire.performed += OnJump;
        
    }

    void Update() {
        
        CheckIfGrounded(); 

        ApplySideMovement();

        timeSinceLastJump += Time.deltaTime;
        timeSinceLastGrav += Time.deltaTime;

        if(!gravReady && timeSinceLastGrav > gravCoolDown) {
            gravReady = true;
            OnGravStatusChange?.Invoke(gravReady);
        }
    }

    void FixedUpdate() {
        //Apply Gravity
        rigidBody.AddForce(transform.up * Physics.gravity.y * rigidBody.mass);
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == terrainLayer) {
            isGrounded = true;
        }
    }

    void ApplySideMovement() {
        float value = 0;

        if(gravityState == Direction.Up || gravityState == Direction.Down) {
            value = playerControls.Player.XAxisMove.ReadValue<float>();
            
            Vector3 delta = value * moveSpeed * Time.deltaTime * Vector2.right;
            Vector3 newPosition = transform.position + delta;

            transform.position = newPosition;
        }
        else {
            value = playerControls.Player.YAxisMove.ReadValue<float>();

            Vector3 delta = value * moveSpeed * Time.deltaTime * Vector2.up;
            Vector3 newPosition = transform.position + delta;

            transform.position = newPosition;
        }

        //For flipping sprite based on direction
        #region 
        if (gravityState == Direction.Right || gravityState == Direction.Down) {
            if (value > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (value < 0)
            {
                spriteRenderer.flipX = true;
            }
        } else {
            if (value > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (value < 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        #endregion

        if(value != 0 && isGrounded) {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

   

    void OnSwitchGravity(InputAction.CallbackContext context) {
        if(GravityEnabled) {
            if (timeSinceLastGrav > gravCoolDown)
            {
                switch (context.ReadValue<Vector2>().x)
                {
                    case 1:
                        ChangeGravity(Direction.Right);
                        break;
                    case -1:
                        ChangeGravity(Direction.Left);
                        break;
                }

                switch (context.ReadValue<Vector2>().y)
                {
                    case 1:
                        ChangeGravity(Direction.Up);
                        break;
                    case -1:
                        ChangeGravity(Direction.Down);
                        break;
                }
            }
        }
    }
    
    void OnJump(InputAction.CallbackContext context) {
        if(BootsEnabled) {
            if (isGrounded)
            {
                rigidBody.AddForce(transform.up * groundBoost, ForceMode2D.Impulse);
                OnFireEvent?.Invoke();

                timeSinceLastJump = 0;
            }
            else
            {
                if(timeSinceLastJump > jumpCoolDown) {
                    rigidBody.AddForce(transform.up * shootBoost, ForceMode2D.Impulse);

                    OnFireEvent?.Invoke();

                    timeSinceLastJump = 0;
            
                }
                
            }
        }
        
    }


    void ChangeGravity(Direction gravityState) {
        
            this.gravityState = gravityState;

            switch (gravityState)
            {
                case Direction.Down:
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                case Direction.Left:
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                    break;
                case Direction.Right:
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    break;
                case Direction.Up:
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
            }


            rigidBody.velocity = Vector2.zero;
            timeSinceLastGrav = 0;
            gravReady = false;
            OnGravStatusChange?.Invoke(gravReady);
        
     
    }

    void CheckIfGrounded() {
        if (groundCollider != null)
        {
            if (groundCollider.IsTouchingLayers(terrainLayer))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

    }

    public void ApplyKnockBack(Vector3 knockbackDirection) {
       // rigidBody.AddForce( knockbackDirection * knockbackForce, ForceMode2D.Impulse );
    }
}

public enum Direction {
    Up, Down, Left, Right
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float groundBoost = 5;
    [SerializeField] float shootBoost = 2;

    [SerializeField] float jumpCoolDown = .3f;
    
    [SerializeField] Collider2D groundCollider;
    [SerializeField] LayerMask terrainLayer;

    public event Action OnFireEvent;

    public bool GravityEnabled {get; set;}
    public bool BootsEnabled {get; set;}

    Animator animator;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    PlayerControls playerControls;
    List<Vector2> DirectionVectors;
    GravityState gravityState;


    bool isGrounded;
    float timeSinceLastJump;
    

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
        //set initial gravity to down
        ChangeGravity(GravityState.Down);

        //Turns off equipment movement
        GravityEnabled = false;
        BootsEnabled = false;


        timeSinceLastJump = float.MaxValue;
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

        if(gravityState == GravityState.Up || gravityState == GravityState.Down) {
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
        if (gravityState == GravityState.Right || gravityState == GravityState.Down) {
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
            switch (context.ReadValue<Vector2>().x)
            {
                case 1:
                    ChangeGravity(GravityState.Right);
                    break;
                case -1:
                    ChangeGravity(GravityState.Left);
                    break;
            }

            switch (context.ReadValue<Vector2>().y)
            {
                case 1:
                    ChangeGravity(GravityState.Up);
                    break;
                case -1:
                    ChangeGravity(GravityState.Down);
                    break;
            }
        }
    }
    
    void OnJump(InputAction.CallbackContext context) {
        if(BootsEnabled) {
            if (isGrounded)
            {
                rigidBody.AddForce(transform.up * groundBoost, ForceMode2D.Impulse);
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


    void ChangeGravity(GravityState gravityState) {

        this.gravityState = gravityState;

        switch(gravityState) {
            case GravityState.Down: 
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case GravityState.Left:
                transform.rotation = Quaternion.Euler(0,0,-90);
                break;
            case GravityState.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case GravityState.Up:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
        
        
        rigidBody.velocity = Vector2.zero;
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
}

public enum GravityState {
    Up, Down, Left, Right
}
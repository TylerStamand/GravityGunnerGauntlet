using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float verticalMult = 5;

    Rigidbody2D rigidBody;
    PlayerControls playerControls;
    List<Vector2> DirectionVectors;
    GravityState gravityState;
    

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        playerControls = new PlayerControls();
        

        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
        ChangeGravity(GravityState.Down);
    }

    void OnEnable() {
        playerControls.Enable();
    }

    void Start() {
        playerControls.Player.SwitchGravity.performed += OnSwitchGravity;
        playerControls.Player.Fire.performed += OnFire;
    }

    void Update() {
        
        // if(Input.GetKeyDown(KeyCode.LeftArrow)) {
        //     ChangeGravity(-90);
        // }
        // if(Input.GetKeyDown(KeyCode.RightArrow)) {
        //     ChangeGravity(90);
        // }
        // if(Input.GetKeyDown(KeyCode.UpArrow)) {
        //     ChangeGravity(180);
        // }
        
        
       ApplySideMovement();

    }

    void FixedUpdate() {
        //Apply Gravity
        
        rigidBody.AddForce(transform.up * Physics.gravity.y * rigidBody.mass);
    }

    void OnDisable() {
        playerControls.Disable();
    }

    void ApplySideMovement() {
        

        if(gravityState == GravityState.Up || gravityState == GravityState.Down) {
            float value = playerControls.Player.XAxisMove.ReadValue<float>();

            Vector3 delta = value * moveSpeed * Time.deltaTime * Vector2.right;
            Vector3 newPosition = transform.position + delta;

            transform.position = newPosition;
        }
        else {
            float value = playerControls.Player.YAxisMove.ReadValue<float>();

            Vector3 delta = value * moveSpeed * Time.deltaTime * Vector2.up;
            Vector3 newPosition = transform.position + delta;

            transform.position = newPosition;
        }
    }

   

    void OnSwitchGravity(InputAction.CallbackContext context) {
        switch(context.ReadValue<Vector2>().x) {
            case 1 :
                ChangeGravity(GravityState.Right);
                break;
            case -1 :
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
    
    void OnFire(InputAction.CallbackContext context) {
        rigidBody.AddForce(transform.up * verticalMult, ForceMode2D.Impulse);
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


}

public enum GravityState {
    Up, Down, Left, Right
}
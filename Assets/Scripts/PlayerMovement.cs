using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float horizontalMoveSpeed = 3;
    [SerializeField] float verticalMult = 700;

    Rigidbody2D rigidBody;
    
    List<Vector2> DirectionVectors;

    int currentDirection = 0;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();

        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;


        DirectionVectors = new List<Vector2> {new Vector2(0,1), new Vector2(1,0), new Vector2(0,-1), new Vector2(-1,0)};

    }

    void Update() {
        ApplyHorizontalMovement();
        ApplyVerticalMovement();

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            ChangeGravity();
        }
    }

    void FixedUpdate() {
        //Apply Gravity
        Vector2 direction = DirectionVectors[currentDirection];
        rigidBody.AddForce(direction * Physics.gravity.y * rigidBody.mass);
    }

    void ApplyHorizontalMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 delta = horizontal * horizontalMoveSpeed * Time.deltaTime * transform.right;
        Vector3 newPosition = transform.position + delta;
       
        transform.position = newPosition;
    }

    void ApplyVerticalMovement() {
        if(Input.GetButton("Jump")) {
            float YForce = verticalMult * Time.deltaTime;
            rigidBody.AddForce(transform.up * YForce, ForceMode2D.Force);
        }
        
    }

    void ChangeGravity() {
        currentDirection = (currentDirection + 1) % DirectionVectors.Count;
        transform.Rotate(0, 0, -90);
    }

}

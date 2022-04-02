using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float horizontalMoveSpeed = 3;
    [SerializeField] float verticalMult = 700;

    Rigidbody2D rigidBody;
    
    List<Vector2> DirectionVectors;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();

        //turn off default rigidBody Gravity;
        rigidBody.gravityScale = 0;
    }

    void Update() {
        ApplyHorizontalMovement();
        ApplyVerticalMovement();

        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            ChangeGravity(-90);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            ChangeGravity(90);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            ChangeGravity(180);
        }
    }

    void FixedUpdate() {
        //Apply Gravity
        
        rigidBody.AddForce(transform.up * Physics.gravity.y * rigidBody.mass);
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

    void ChangeGravity(float angle) {
        transform.Rotate(0, 0, angle);
        rigidBody.velocity = Vector2.zero;
    }

}

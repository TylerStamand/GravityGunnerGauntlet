using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float horizontalMoveSpeed = 3;
    [SerializeField] float verticalMult = 700;

    Rigidbody2D rigidBody;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        ApplyHorizontalMovement();
        ApplyVerticalMovement();
    }

    void ApplyHorizontalMovement() {
        float horizontal = Input.GetAxis("Horizontal");
        float deltaX = horizontal * horizontalMoveSpeed * Time.deltaTime;
        Vector3 newPosition = new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z);
       
        transform.position = newPosition;
    }

    void ApplyVerticalMovement() {
        if(Input.GetButton("Jump")) {
            float YForce = verticalMult * Time.deltaTime;
            rigidBody.AddForce(transform.up * YForce, ForceMode2D.Force);
        }
        
    }
}

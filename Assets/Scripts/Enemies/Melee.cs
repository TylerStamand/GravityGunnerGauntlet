using UnityEngine;

public class Melee : Enemy {

    [SerializeField] float moveSpeed = 3;
    [SerializeField] float moveRadius = 2;
    
    Vector3 startPos;
    bool moveRight;

    void Awake() {
        startPos = transform.position;
    }
    
    void Update() {
       Move();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

        if(player != null) {
            player.TakeDamage(1);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Vector3 leftBound = startPos - transform.right * moveRadius;
        Vector3 rightBound =  startPos + transform.right * moveRadius;
        Gizmos.DrawLine(leftBound, rightBound);
    }

    void Move() {
        float startX = startPos.x;
        if (transform.position.x < startX - moveRadius)
        {
            moveRight = true;
        }
        if (transform.position.x < startX + moveRadius)
        {
            moveRight = false;
        }

        if (moveRight)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
        }
    }

}

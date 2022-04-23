using System;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] Weapon weapon;

    [Header("Walking")]
    [SerializeField] float walkSpeed = 1;

    [Header("Charging")]
    [SerializeField] float chargeVelocity = 1;
    [SerializeField] float chargeCoolDown = 10;
    [SerializeField] float fastChargeCoolDown = 5;
    

    [Header("Shooting")]
    [SerializeField] float shootCoolDown = 2;
    [SerializeField] float fastShootCoolDown = 1; 

    public Direction CurrentDirection {get; private set;}

    PlayerUnit player;
    new Rigidbody2D rigidbody;


    float timeSinceLastCharge;
    float timeSinceLastShot;
    bool spriteFlipDefault;
    bool isCharging;
    bool isWalking;
    int walkingDir;
    int phase;

    protected override void Awake() {
        base.Awake();

        player = FindObjectOfType<PlayerUnit>();
        rigidbody = GetComponent<Rigidbody2D>();

        rigidbody.gravityScale = 0;

        timeSinceLastCharge = float.MaxValue;
        timeSinceLastShot = float.MaxValue;
        spriteFlipDefault = spriteRenderer.flipX;
        isCharging = false;
        phase = 0;
    }

    protected override void Update()
    {
        base.Update();
        timeSinceLastCharge += Time.deltaTime;
        timeSinceLastShot += Time.deltaTime;

        if(phase == 0) {
            if (timeSinceLastCharge >= chargeCoolDown && !isCharging)
            {
                Charge();
                timeSinceLastCharge = 0;
            }
        }

        else if(phase == 1) {
            if(timeSinceLastCharge >= chargeCoolDown && !isCharging) {
                Charge();
                timeSinceLastCharge = 0;
            }
            if(timeSinceLastShot >= shootCoolDown && !isCharging) {
                weapon.transform.up = player.transform.position - weapon.transform.position;
                weapon.Shoot();
                timeSinceLastShot = 0;
            }

        }

        else if(phase == 2) {
            if(timeSinceLastCharge >= fastChargeCoolDown && !isCharging) {
                Charge();
                timeSinceLastCharge = 0;
            }
            if(timeSinceLastShot >= fastShootCoolDown && !isCharging) {
                weapon.transform.up = player.transform.position - weapon.transform.position;
                weapon.Shoot();
                timeSinceLastShot = 0;
            }
        }
    }

    void FixedUpdate() {
        if(isCharging) {
            rigidbody.velocity = transform.right * chargeVelocity;
        }
        if(isWalking) {
            if(walkingDir == 0) {
                rigidbody.velocity = transform.right * walkSpeed;
            } else {
                rigidbody.velocity = -transform.right * walkSpeed;
            }
            
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();

        if (player != null)
        {
            player.TakeDamage(1, transform.position - collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {

        if(isCharging) {
            BossWall bossWall = collider.gameObject.GetComponent<BossWall>();
            if (bossWall != null)
            {
                bossWall.ActivateElectricity();
                EndCharge();

                //start walking, ik this logic is hard to follow im doing my best
                walkingDir= new System.Random().Next(0, 1);
                Walk(walkingDir);


            }
        }

        if(isWalking) {
            BossWall bossWall = collider.gameObject.GetComponent<BossWall>();

            //This flips the walking direction with the boss hits a wall thats it's not currently standing on
            if(bossWall != null && bossWall.Side != CurrentDirection) {
                walkingDir = walkingDir == 1 ? 0 : 1;
                Walk(walkingDir);
            }
            
        }

     
        // Boss boss = collider.gameObject.GetComponent<Boss>();
        // if (boss != null && Side == boss.CurrentDirection)
        // {
        //     boss.EndCharge();
        //     electicityOn = true;
        //     timeElectricityOn = 0;
        // }
    }

    void Walk(int dir) {

        //picks random dir, right if 1, left if 0
        animator.SetBool("walking", true);
        isWalking = true;
        if(dir == 1) {
            spriteRenderer.flipX = false;
           
        }
        else {
            spriteRenderer.flipX = true;
        }

    }

    void Charge() {
        Direction playerGravityState = player.GetGravityState();
        CurrentDirection = playerGravityState;
        isCharging = true;
        isWalking = false;
        spriteRenderer.flipX = spriteFlipDefault;
        animator.SetBool("walking", false);
        animator.SetBool("charging", true);
        
        switch (playerGravityState)
        {
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }

     
        
    
    }

    public void EndCharge() {
        if (isCharging)
        {
            Vector3 angles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(angles.x, angles.y, angles.z + 90);
            transform.rotation = Quaternion.Euler(angles.x, angles.y, angles.z + 90);
            rigidbody.velocity = Vector2.zero;
            isCharging = false;
            animator.SetBool("charging", false);
            
            
        }
    }


    public override void TakeDamage(int damage, Vector3 knockbackVector) {
        base.TakeDamage(damage, knockbackVector);
        
        //Changes Boss Phase if below a certain health
        if(currentHealth / (float)(maxHealth) < .33f) {
            phase = 2;
        }
        else if(currentHealth / (float)(maxHealth) < .66f) {
            phase = 1;
        }

    }

}

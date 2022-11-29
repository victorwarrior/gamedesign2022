using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem dustWalk;
    public ParticleSystem dustSpeed;
    public ParticleSystem dustJump;


    // constants
    public string playerIdentity;
    public GameObject otherPlayer;


    public string keyLeft, keyRight, keyJump, keyRestart;

    public float speed;
    public float maxSpeedBigConstant = 4.2f;
    public float maxSpeedSmallConstant = 5.8f;
    public float maxSpeed;
    public float jumpForce;

    public float dragAmount;
    public float dragAmountUp;

    public float deceleration; // @NOTE: 1.32 seems good. don't set below 1.0 -Victor

    // other variables
    Rigidbody2D rb;
    Rigidbody2D otherRb;


    public bool jump = false;
    public bool onGround;
    public int keys1 = 0;
    public int keys2 = 0;
    public int keys3 = 0;
    public int direction;
    public int health = 100;
    public int healthLossAmount = 10;

    // debug variables
    //public float xVelocityCheck = 0f;
    public int testInteger = 0;



    void Start() {
        rb = GetComponent<Rigidbody2D>();
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
        playerIdentity = playerIdentity.ToLower();
      
        rb.drag = 0.25f;
    }


    void Update() {

        // jumping
        if (Input.GetKeyDown(keyJump) && onGround) {
            jump = true;
        }

        // restarting level
        if (Input.GetKeyDown(keyRestart)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void FixedUpdate() {
        direction = 0;
        if (Input.GetKey(keyLeft)) direction = -1;
        if (Input.GetKey(keyRight)) direction = 1;
        if (Input.GetKey(keyLeft) && Input.GetKey(keyRight)) direction = 0;

 
        if (playerIdentity == "big")   maxSpeed = maxSpeedBigConstant;
        if (playerIdentity == "small") maxSpeed = maxSpeedSmallConstant;

        if (direction != 0) {
            // movement
            rb.AddForce(Vector2.right * direction * speed);
            CreateDustWalk();
            CreateDustSpeed();
            if (rb.velocity.x > maxSpeed)  rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            if (rb.velocity.x < -maxSpeed) rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        } else if ((Vector3.Distance(gameObject.transform.position, otherPlayer.transform.position) > 8.9) && (onGround == false || otherPlayer.GetComponent<PlayerController>().onGround == false)) { // check if Vector2.Distance can be used instead -Victor
            // swinging
            maxSpeed *= 2;
            Debug.Log("TEST");
        } else {
            // deceleration
            rb.velocity = new Vector2(rb.velocity.x / deceleration, rb.velocity.y);
        }

        // jump
        if (jump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            CreateDustWalk();
            CreateDustSpeed();
            jump = false;
            onGround = false;
        }

        // debug
        //xVelocityCheck = rb.velocity.x; // @DEBUG: used to monitor the speed of the player in the inspector -Victor

    }
    

    private void OnTriggerEnter2D(Collider2D collision) {

        // death trigger
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            print("deathTrigger");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onGround == true)
        {
            dustJump.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {

        // drag down force
        if (playerIdentity == "big") {
            if (collision.gameObject.CompareTag("DragdownRight") && otherPlayer.GetComponent<PlayerController>().onGround == true) {
                otherRb.AddForce(Vector2.right * dragAmount, ForceMode2D.Impulse);
            }
        }

        if (playerIdentity == "big") {
            if (collision.gameObject.CompareTag("DragdownLeft") && otherPlayer.GetComponent<PlayerController>().onGround == true) {
                otherRb.AddForce(Vector2.left * dragAmount, ForceMode2D.Impulse);
            }
        }

        // drag up force
        if (playerIdentity == "small" && onGround == false) {
            if (collision.gameObject.CompareTag("DragUp") && otherPlayer.GetComponent<PlayerController>().onGround == true) {
                if ((transform.position.x > otherPlayer.transform.position.x && otherPlayer.GetComponent<PlayerController>().direction == -1) || (transform.position.x < otherPlayer.transform.position.x && otherPlayer.GetComponent<PlayerController>().direction == 1)) {
                    rb.AddForce(Vector2.up * dragAmountUp, ForceMode2D.Force);
                }
            }
        }

    }

    // pushing stone (commented out because big instead has permanent super speed)
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerIdentity == "big" && collision.gameObject.CompareTag("Stone"))
        {
            if ((collision.transform.position.x > gameObject.transform.position.x) && direction == 1)
            {
                speed = 350;
            }
            if ((collision.transform.position.x < gameObject.transform.position.x) && direction == -1)
            {
                speed = 350;
            }
        }
    }*/



    public void CreateDustWalk()
    {
        dustWalk.Play();
    }

    public void CreateDustSpeed()
    {
        dustSpeed.Play();
    }

    public void CreateDustJump()
    {
        dustJump.Play();
    }
}

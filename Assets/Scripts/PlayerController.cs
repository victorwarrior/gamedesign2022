using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    // constants
    public string playerIdentity;
    public GameObject otherPlayer;

    public string keyLeft, keyRight, keyJump, keyRestart;

    public float speed;
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
    public bool hasKey = false;
    public int direction;

    // debug variables
    //public float xVelocityCheck = 0f;



    void Start() {
        rb = GetComponent<Rigidbody2D>();
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();

        playerIdentity = playerIdentity.ToLower();
    }


    void Update() {
        // jumping
        if (Input.GetKeyDown(keyJump) && onGround) {
            jump = true;
        }

        // restarting level
        if (Input.GetKeyDown(keyRestart)) {
            SceneManager.LoadScene("MainScene");
        }

    }

    private void FixedUpdate() {
        direction = 0;
        if (Input.GetKey(keyLeft)) direction = -1;
        if (Input.GetKey(keyRight)) direction = 1;
        if (Input.GetKey(keyLeft) && Input.GetKey(keyRight)) direction = 0;

        rb.drag = 0.0f;

        if (direction != 0) {
            // movement
            rb.AddForce(Vector2.right * direction * speed);
            if (rb.velocity.x > maxSpeed)  rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            if (rb.velocity.x < -maxSpeed) rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        } else if (Vector3.Distance(gameObject.transform.position, otherPlayer.transform.position) <= 8.9) { // check if Vector2.Distance can be used instead -Victor
            // deceleration
            rb.velocity = new Vector2(rb.velocity.x / deceleration, rb.velocity.y);
        } else {
            // drag when swinging
            rb.drag = 0.25f;
        }

        // jump
        if (jump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        // debug
        //xVelocityCheck = rb.velocity.x; // @DEBUG: used to monitor the speed of the player in the inspector -Victor

    }
    

    private void OnTriggerEnter2D(Collider2D collision) {

        // death trigger
        if (collision.gameObject.CompareTag("DeathTrigger")) SceneManager.LoadScene("MainScene");

        // player head collision
        switch (playerIdentity) {
            case "big":
                if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = true;
                break;

            case "small":
                if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = true;
                break;
                
        }  

    }

    private void OnTriggerStay2D(Collider2D collision){

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

        if (playerIdentity == "small" && onGround == false) {
            if (collision.gameObject.CompareTag("DragUp") && otherPlayer.GetComponent<PlayerController>().onGround == true) {
                if ((transform.position.x > otherPlayer.transform.position.x && otherPlayer.GetComponent<PlayerController>().direction == -1) || (transform.position.x < otherPlayer.transform.position.x && otherPlayer.GetComponent<PlayerController>().direction == 1)) {
                    rb.AddForce(Vector2.up * dragAmountUp, ForceMode2D.Force);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        // player head collision
        if (transform.position.y > otherPlayer.transform.position.y) {
            switch (playerIdentity) {
                case "big":
                    if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = false;
                    break;

                case "small":
                    if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = false;
                    break;

            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")) onGround = true;

    }


    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")) onGround = false;
    }

    public void keyTrue()
    {
        hasKey = true;
    }

}

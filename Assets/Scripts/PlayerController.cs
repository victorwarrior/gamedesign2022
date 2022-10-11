using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    // constants
    public string playerIdentity;
    public GameObject otherPlayer;

    public string keyLeft, keyRight, keyJump;

    public float speed;
    public float maxSpeed;
    public float jumpForce;
    public float deceleration; // @NOTE: 1.32 seems good. don't set below 1 -Victor

    // other variables
    public bool  jump = false;
    public bool  onGround;

    // debug variables:
    //public float xVelocityCheck = 0f;



    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerIdentity = playerIdentity.ToLower();
    }


    void Update() {
        // jumping
        if (Input.GetKeyDown(keyJump) && onGround) {
            jump = true;
        }
    }

    private void FixedUpdate() {
        int direction = 0;
        if (Input.GetKey(keyLeft)) direction = -1;
        if (Input.GetKey(keyRight)) direction = 1;
        if (Input.GetKey(keyLeft) && Input.GetKey(keyRight)) direction = 0;

        if (direction != 0) {
            // movement
            rb.AddForce(Vector2.right * direction * speed);
            if (rb.velocity.x > maxSpeed)  rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            if (rb.velocity.x < -maxSpeed) rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        } else {
            // deceleration 
            rb.velocity = new Vector2(rb.velocity.x/deceleration, rb.velocity.y);
        }


        // jump
        if (jump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        //xVelocityCheck = rb.velocity.x; // @DEBUG: used to monitor the speed of the player in the inspector -Victor
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("DeathTrigger")) SceneManager.LoadScene("MainScene");

        // "head" collision to enable jumping
        if (playerIdentity == "big") {
            if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = true; 
        } else if (playerIdentity == "small") {
            if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = true;
        } else {
            Debug.Log("Wrong playerIdentity value on " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        // "head" collision to enable jumping
        if (transform.position.y > otherPlayer.transform.position.y) {
            if (playerIdentity == "big") {
                if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = false;
            } else if (playerIdentity == "small") {
                if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = false;
            } else {
                Debug.Log("Wrong playerIdentity value on " + gameObject.name);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")) onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")) onGround = false;
    }

}

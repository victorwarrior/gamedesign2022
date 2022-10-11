using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    public string keyLeft  = "left"; // @NOTE: is currently used to distinquise between Big and Small -Victor
    public string keyRight = "right";
    public string keyJump  = "up";

    public float speed     = 10;
    public float maxSpeed  = 10;
    public float jumpForce = 10;
    public float deceleration = 1.32f; // @NOTE: don't set below 1 -Victor

    public bool  jump      = false;
    public bool  onGround;

    // debug variables:
    //public float xVelocityCheck = 0f;





    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
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
            if (rb.velocity.x > maxSpeed) rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
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

        //xVelocityCheck = rb.velocity.x; // @DEBUG
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("DeathTrigger")) SceneManager.LoadScene("MainScene");

        if (keyLeft == "left")
        {
            if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = true;
            

        }
        else if (keyLeft == "a")
        {
            if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = true;
        }
        else
        {
            Debug.Log("Bug.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (keyLeft == "left")
        {
            if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = false;
        }
        else if (keyLeft == "a")
        {
            if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = false;
        }
        else
        {
            Debug.Log("Bug: players are not called PlayerSmall & PlayerBig.");
        }


    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")) onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Platform")) onGround = false;
    }

}

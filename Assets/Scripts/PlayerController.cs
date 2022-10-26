using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    //add force to small player
    public Rigidbody2D otherRb; // misleading comment, force is not added here. -Victor
    

    // constants
    public string playerIdentity;
    public GameObject otherPlayer;

    public string keyLeft, keyRight, keyJump, keyRestart;

    public float speed;
    public float maxSpeed;
    public float jumpForce;

    public float dragAmount;

    public float deceleration; // @NOTE: 1.32 seems good. don't set below 1 -Victor

    // other variables
    public bool  jump = false;
    public bool  onGround;
    public bool hasKey = false;

    // debug variables:
    //public float xVelocityCheck = 0f;
    //LineRenderer ropeLineRenderer; //@NOTE: remove lineRenderer components on objects when removing this -Victor



    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerIdentity = playerIdentity.ToLower();
        // try adding otherRb here instead? -Victor
        //ropeLineRenderer = GetComponent<LineRenderer>();

        
    }


    void Update() {
        // jumping
        if (Input.GetKeyDown(keyJump) && onGround) {
            jump = true;
        }

        if (Input.GetKeyDown(keyRestart)) SceneManager.LoadScene("MainScene");


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

        }
        else if(direction == 0 && onGround){

            // deceleration
            rb.velocity = new Vector2(rb.velocity.x / deceleration, rb.velocity.y);

        }
        else //direction == 0 && !onGrund also means that jumping without direction will induce drag - Vic
        {
            //decelerate swinging motion
            rb.drag = 0.25f; // @PROBLEM: makes jumping weird, should probably only apply when ~dist_between_players = max. also could use drag instead of deceleration? -Victor 
            
        }


        // jump
        if (jump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        // debug
        //xVelocityCheck = rb.velocity.x; // @DEBUG: used to monitor the speed of the player in the inspector -Victor
        //ropeLineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        //ropeLineRenderer.SetPosition(1, new Vector3(otherPlayer.transform.position.x, otherPlayer.transform.position.y, otherPlayer.transform.position.z));


    }
    

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("DeathTrigger")) SceneManager.LoadScene("MainScene");

        // "head" collision to enable jumping
        if (playerIdentity == "big") { // can make smaller -Victor
            if (collision.gameObject.CompareTag("PlayerSmallHead")) onGround = true; 
        } else if (playerIdentity == "small") {
            if (collision.gameObject.CompareTag("PlayerBigHead")) onGround = true;
        } else {
            Debug.Log("Wrong playerIdentity value on " + gameObject.name);
        }

        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerIdentity == "big")
        {
            
            if (collision.gameObject.CompareTag("DragdownRight")&& otherPlayer.GetComponent<PlayerController>().onGround == true)
            {
                otherRb.AddForce(Vector2.right * dragAmount, ForceMode2D.Impulse);
            }
        }

        if (playerIdentity == "big")
        {

            if (collision.gameObject.CompareTag("DragdownLeft") && otherPlayer.GetComponent<PlayerController>().onGround == true)
            {
                otherRb.AddForce(Vector2.left * dragAmount, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        // "head" collision to enable jumping
        if (transform.position.y > otherPlayer.transform.position.y) { // @NOTE: if theres trouble maybe change to > y + height/2. -Victor
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

    public void keyTrue()
    {
        hasKey = true; // i don't this method is needed. -Victor
    }

}

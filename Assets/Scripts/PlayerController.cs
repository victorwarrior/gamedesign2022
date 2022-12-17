using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    // constant variables
    public string playerIdentity;
    public string keyLeft, keyRight, keyDown, keyJump, keyRestart;

           float speedBigConstant      = 350.0f;
           float speedSmallConstant    = 60.8f;
           float maxSpeedBigConstant   = 4.2f;
           float maxSpeedSmallConstant = 7.0f;
    public float jumpForce;

    public float dragAmount;
    public float dragAmountUp;

           float decelerationConstant  = 1.32f; // @NOTE: don't set below 1.0 -Victor

    // reference variables
    public GameObject otherPlayer;
           Rigidbody2D rb;
           Rigidbody2D otherRb;

    public ParticleSystem dustWalk;
    public ParticleSystem dustSpeed;
    public ParticleSystem dustJump;
    public Animator squashStrechAnimation;

    // other variables
    public bool jump;
    public bool onGround;
    public bool swinging;
           float speed;
           float maxSpeed;
    public float distanceBetweenPlayers = 4.5f;
    public int keysYellow = 0;
    public int keysGreen  = 0;
    public int keysBlue   = 0;
    public int direction;
    public int verticalDir;

    private BoxCollider2D boxCol;
    private DistanceJoint2D distjoint;
    private LineRenderer lineRend;

    public AudioClip JumpSound;


    //Vector3 tempPos;
    //public GameObject playerMouth;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        distjoint = GetComponent<DistanceJoint2D>();
        lineRend = GetComponent<LineRenderer>();


        rb.drag = 0.25f;
        otherRb = otherPlayer.GetComponent<Rigidbody2D>();
        playerIdentity = playerIdentity.ToLower();
        jump = false;
        if (playerIdentity == "big")   speed = speedBigConstant;
        if (playerIdentity == "small") speed = speedSmallConstant;

        

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

        /*
        if(playerIdentity == "small")
        {
            if (direction == -1)
            {
                playerMouth.transform.position = new Vector3(gameObject.transform.position.x - 0.65f, playerMouth.transform.position.y, playerMouth.transform.position.z);
            }

            if (direction == 1)
            {
                playerMouth.transform.position = new Vector3(gameObject.transform.position.x - 0.3f, playerMouth.transform.position.y, playerMouth.transform.position.z);
            }

            if (direction == 0)
            {
                playerMouth.transform.position = new Vector3(gameObject.transform.position.x - 0.5f, playerMouth.transform.position.y, playerMouth.transform.position.z);
            }
        }

        if (playerIdentity == "big")
        {
            if (direction == -1)
            {
                playerMouth.transform.position = new Vector3(gameObject.transform.position.x - 0.4f, playerMouth.transform.position.y, playerMouth.transform.position.z);
            }

            if (direction == 1)
            {
                playerMouth.transform.position = new Vector3(gameObject.transform.position.x + 0.4f, playerMouth.transform.position.y, playerMouth.transform.position.z);
            }

            if (direction == 0)
            {
                playerMouth.transform.position = new Vector3(gameObject.transform.position.x + 0.1f, playerMouth.transform.position.y, playerMouth.transform.position.z);
            }
        }
        */

    }


    private void FixedUpdate() {
        //
        verticalDir = 0;
        if (Input.GetKey(keyJump)) verticalDir += -1;
        if (Input.GetKey(keyDown)) verticalDir +=  1;
        direction = 0;
        if (Input.GetKey(keyLeft))  direction += -1;
        if (Input.GetKey(keyRight)) direction +=  1;

        if (playerIdentity == "big")   maxSpeed = maxSpeedBigConstant;
        if (playerIdentity == "small") maxSpeed = maxSpeedSmallConstant;
        
        float offset = 0.0f; // @NOTE: the center of the small player is somehow wrong. this offset makes the distance calculated below correct. -Victor
        if (playerIdentity == "big")   offset =  0.5f;
        if (playerIdentity == "small") offset = -0.5f;
        distanceBetweenPlayers = Vector3.Distance(new Vector3(transform.position.x + offset, transform.position.y, transform.position.z), otherPlayer.transform.position);

        // swinging
        swinging = false;        
        if (  (distanceBetweenPlayers > 8.9f)
           && (onGround == false)
         //&& (otherPlayer.GetComponent<PlayerController>().onGround == true)
           && (transform.position.y < otherPlayer.transform.position.y)  ) {
            swinging = true;
            //maxSpeed *= 1.7f;
        }

        if (swinging == true) {
            // unable to move
        } else if (direction != 0) {
            // move
            rb.AddForce(Vector2.right * direction * speed);
            if (rb.velocity.x > maxSpeed)  rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            if (rb.velocity.x < -maxSpeed) rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);

            CreateDustWalk();
            CreateDustSpeed();
        } else {
            // deceleration
            if (swinging == false) rb.velocity = new Vector2(rb.velocity.x / decelerationConstant, rb.velocity.y);
        }

        // jump
        if (jump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            gameObject.GetComponent<AudioSource>().PlayOneShot(JumpSound);
            jump = false;
            onGround = false;

            CreateDustWalk();
            CreateDustSpeed();
            squashStrechAnimation.SetTrigger("Jump");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {

        // death trigger
        if (collision.gameObject.CompareTag("DeathTrigger"))
        {
            print("deathTrigger");
            SetactiveFalse();
            Invoke("RestartLevel",0.7f);
            squashStrechAnimation.SetTrigger("Death");

            gameObject.GetComponent<PlayerController>().SetactiveFalse();
            gameObject.GetComponent<PlayerController>().otherPlayer.GetComponent<PlayerController>().SetactiveFalse();

            gameObject.GetComponent<PlayerController>().squashStrechAnimation.SetTrigger("Death");
            gameObject.GetComponent<PlayerController>().otherPlayer.GetComponent<PlayerController>().squashStrechAnimation.SetTrigger("Death");

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                if (gameObject.transform.GetChild(i).gameObject.CompareTag("Eyes")) gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < gameObject.GetComponent<PlayerController>().otherPlayer.transform.childCount; i++)
            {
                if (gameObject.GetComponent<PlayerController>().otherPlayer.transform.GetChild(i).gameObject.CompareTag("Eyes")) gameObject.GetComponent<PlayerController>().otherPlayer.transform.GetChild(i).gameObject.SetActive(false);
            }
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

    public void CreateDustWalk() {
        dustWalk.Play();
    }

    public void CreateDustSpeed() {
        dustSpeed.Play();
    }

    public void CreateDustJump() {
        dustJump.Play();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void SetactiveFalse()
    {
        distjoint.enabled = false;
        boxCol.enabled = false;
        lineRend.enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
    }

}

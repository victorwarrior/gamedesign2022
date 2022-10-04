using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBigController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed     = 10;
    public float maxSpeed  = 10;
    public float jumpForce = 10;
    public bool  jump      = false;
    public bool  onGround;

    public string keyLeft  = "left";
    public string keyRight = "right";
    public string keyJump  = "up";

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(keyJump) && onGround) {
            jump = true;
        }
    }

    private void FixedUpdate() {
        int direction = 0;
        if (Input.GetKey(keyLeft))  direction = -1;
        if (Input.GetKey(keyRight)) direction = 1;
        if (Input.GetKey(keyLeft) && Input.GetKey(keyRight)) direction = -1;

        rb.AddForce(Vector2.right * direction * speed);

        if (rb.velocity.x > maxSpeed)  rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        if (rb.velocity.x < -maxSpeed) rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);

        if (jump) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DeathTrigger")) {
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("Player")) {
            onGround = true; // TODO: dette gør vel at man kan hoppe hvis man rør siden af den anden spiller i luften. -Victor
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) {
            onGround = false;
        }
        if (collision.gameObject.CompareTag("Player")) {
            onGround = false; // mulig bug. -Victor
        }
    }

}

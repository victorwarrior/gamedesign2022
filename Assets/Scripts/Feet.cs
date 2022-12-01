using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("onTriggerEnter Feet");

        if (collision.gameObject.CompareTag("Platform")
         || collision.gameObject.CompareTag("Player")
         || collision.gameObject.CompareTag("Stone")) {
            if (gameObject.GetComponentInParent<PlayerController>().onGround == false) {
                gameObject.GetComponentInParent<PlayerController>().onGround = true;
                gameObject.GetComponentInParent<PlayerController>().dustJump.Play();
                gameObject.GetComponentInParent<PlayerController>().squashStrechAnimation.SetTrigger("Landing");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")
         || collision.gameObject.CompareTag("Player")
         || collision.gameObject.CompareTag("Stone")) {

            gameObject.GetComponentInParent<PlayerController>().onGround = true; // is currently there to fix OnTriggerExit making it impossible to jump

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stone")) {
            gameObject.GetComponentInParent<PlayerController>().onGround = false;
        }
        
    }

}





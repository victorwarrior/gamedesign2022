using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Door;
    public bool onButton = false;
    public int objectsOnButton = 0;
    public AudioClip DoorSound;
    private bool numberCanPlaySound = false;


    private void Update()
    {

        if (objectsOnButton == 0)
        {
            numberCanPlaySound = false;
        }

        if (objectsOnButton == 2)
        {
            numberCanPlaySound = true;
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Stone"))
        {
            Door.gameObject.GetComponent<DoorToLever>().stopUp();

            if (onButton == false && objectsOnButton == 1 && numberCanPlaySound == false)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(DoorSound);

            }

            onButton = true;


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Stone"))
        {
            objectsOnButton++;
        }

    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Stone"))
        {
            Door.gameObject.GetComponent<DoorToLever>().down();
            onButton = false;
            objectsOnButton--;


            if (onButton == false)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }

        }
    }

}

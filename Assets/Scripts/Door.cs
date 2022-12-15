using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int keyType;

    private BoxCollider2D boxCol;

    public AudioClip DoorSound;


    private void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        if (boxCol == null) Debug.Log("no box col");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {


            switch (keyType) {
                case 1:
                    if (collision.gameObject.GetComponent<PlayerController>().keysYellow > 0) {
                        RunDoor();
                        collision.gameObject.GetComponent<PlayerController>().keysYellow--;
                        collision.gameObject.GetComponent<FloatingKeys>().DeactivateFloatingKey(keyType);
                    }
                    break;
                case 2:
                    if (collision.gameObject.GetComponent<PlayerController>().keysGreen > 0) {
                        RunDoor();
                        collision.gameObject.GetComponent<PlayerController>().keysGreen--;
                        collision.gameObject.GetComponent<FloatingKeys>().DeactivateFloatingKey(keyType);
                    }
                    break;
                case 3:
                    if (collision.gameObject.GetComponent<PlayerController>().keysBlue > 0) {
                        RunDoor();
                        collision.gameObject.GetComponent<PlayerController>().keysBlue--;
                        collision.gameObject.GetComponent<FloatingKeys>().DeactivateFloatingKey(keyType);
                    }
                    break;
            }

        } else {
            Debug.Log("no key");
        }

        
    }

    public void sletObject()
    {
        gameObject.SetActive(false);
    }

    private void RunDoor()
    {
        boxCol.enabled = false;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = false;
        }

        gameObject.GetComponent<AudioSource>().PlayOneShot(DoorSound);
        Invoke("sletObject", 5f);
    }
}
        

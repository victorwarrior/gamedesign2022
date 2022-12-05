using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //public ParticleSystem KeyDust;

    public int keyType;

    private SpriteRenderer   spriteRenderer;
    private BoxCollider2D    boxCol;
    private CircleCollider2D circleCol;
    public AudioClip         keySound;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCol         = GetComponent<BoxCollider2D>();
        circleCol      = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            switch (keyType) {
                case 1:
                    other.gameObject.GetComponent<PlayerController>().keysYellow++;
                    break;
                case 2:
                    other.gameObject.GetComponent<PlayerController>().keysGreen++;
                    break;
                case 3:
                    other.gameObject.GetComponent<PlayerController>().keysBlue++;
                    break;
            }

            this.spriteRenderer.enabled = false; // disable the renderer
            if (boxCol    != null) this.boxCol.enabled    = false;
            if (circleCol != null) this.circleCol.enabled = false;

            gameObject.GetComponent<AudioSource>().PlayOneShot(keySound);

            Invoke("sletObject", 5f);
        }
    }

    public void sletObject()
    {
        gameObject.SetActive(false);
    }

}

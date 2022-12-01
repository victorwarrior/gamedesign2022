using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //public ParticleSystem KeyDust;

    public int keyType;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCol;
    public AudioClip keySound;

    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.boxCol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("deactivating key.");

            switch (keyType) {
                case 1:
                    other.gameObject.GetComponent<PlayerController>().keysYellow++;
                    break;
                case 2:
                    other.gameObject.GetComponent<PlayerController>().keysGreen++;
                    break;
            }

            this.spriteRenderer.enabled = false; // disable the renderer
            this.boxCol.enabled = false;

            gameObject.GetComponent<AudioSource>().PlayOneShot(keySound);

            Invoke("sletObject", 5f);
        }
    }

    public void sletObject()
    {
        gameObject.SetActive(false);
    }

}

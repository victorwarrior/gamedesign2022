using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ParticleSystem KeyDust;

    public int keyType;

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCol;


    private void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.boxCol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            CreateKeyDust();
            Debug.Log("deactivating key.");
            switch (keyType) {
                case 1:
                    other.gameObject.GetComponent<PlayerController>().keys1++;
                    break;
                case 2:
                    other.gameObject.GetComponent<PlayerController>().keys2++;
                    break;
                case 3:
                    other.gameObject.GetComponent<PlayerController>().keys3++;
                    break;
            }

            this.spriteRenderer.enabled = false; // disable the renderer
            this.boxCol.enabled = false;
        }
    }

    public void CreateKeyDust()
    {
        KeyDust.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {
    public int keyType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            switch (keyType) {
                case 1:
                    if (collision.gameObject.GetComponent<PlayerController>().keys1 > 0) collision.gameObject.GetComponent<PlayerController>().keys1--;
                    break;
                case 2:
                    if (collision.gameObject.GetComponent<PlayerController>().keys1 > 0) collision.gameObject.GetComponent<PlayerController>().keys1--;
                    break;
                case 3:
                    if (collision.gameObject.GetComponent<PlayerController>().keys1 > 0) collision.gameObject.GetComponent<PlayerController>().keys1--;
                    break;
            }
            gameObject.SetActive(false);

        } else {
            Debug.Log("no key");
        }
    }
}
        

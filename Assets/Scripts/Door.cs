using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int keyType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            switch (keyType) {
                case 1:
                    if (collision.gameObject.GetComponent<PlayerController>().keys1 > 0) {
                        collision.gameObject.GetComponent<PlayerController>().keys1--;
                        gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (collision.gameObject.GetComponent<PlayerController>().keys2 > 0) {
                        collision.gameObject.GetComponent<PlayerController>().keys2--;
                        gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (collision.gameObject.GetComponent<PlayerController>().keys3 > 0) {
                        collision.gameObject.GetComponent<PlayerController>().keys3--;
                        gameObject.SetActive(false);
                    }
                    break;
            }

        } else {
            Debug.Log("no key");
        }
    }
}
        

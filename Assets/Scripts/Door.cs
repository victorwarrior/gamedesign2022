using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public int keyType;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {

            switch (keyType) {
                case 1:
                    if (collision.gameObject.GetComponent<PlayerController>().keysYellow > 0) {
                        collision.gameObject.GetComponent<PlayerController>().keysYellow--;
                        collision.gameObject.GetComponent<FloatingKeys>().DeactivateFloatingKey(keyType);
                        gameObject.SetActive(false);
                    }
                    break;
                case 2:
                    if (collision.gameObject.GetComponent<PlayerController>().keysGreen > 0) {
                        collision.gameObject.GetComponent<PlayerController>().keysGreen--;
                        collision.gameObject.GetComponent<FloatingKeys>().DeactivateFloatingKey(keyType);
                        gameObject.SetActive(false);
                    }
                    break;
                case 3:
                    if (collision.gameObject.GetComponent<PlayerController>().keysBlue > 0) {
                        collision.gameObject.GetComponent<PlayerController>().keysBlue--;
                        collision.gameObject.GetComponent<FloatingKeys>().DeactivateFloatingKey(keyType);
                        gameObject.SetActive(false);
                    }
                    break;
            }

        } else {
            Debug.Log("no key");
        }
    }
}
        

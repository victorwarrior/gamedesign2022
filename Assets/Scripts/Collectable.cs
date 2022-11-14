using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public int keyType;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
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
            gameObject.SetActive(false);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().keys > 0) {
                gameObject.SetActive(false);
                collision.gameObject.GetComponent<PlayerController>().keys--;
            } else {
                Debug.Log("no key");
            }

        }

    }
}
        

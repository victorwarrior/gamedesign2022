using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().hasKey == false)
            {
                Debug.Log("ingen key");
            }

            if (collision.gameObject.GetComponent<PlayerController>().hasKey == true)
            {
                gameObject.SetActive(false);
                collision.gameObject.GetComponent<PlayerController>().hasKey = false;
            }

        }

    }
}
        

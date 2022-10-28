using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{

  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().hasKey == false)
            {
                Debug.Log("ingen key");
            }

            if (other.gameObject.GetComponent<PlayerController>().hasKey == true)
            {
                gameObject.SetActive(false);
            }

        }

    }
}

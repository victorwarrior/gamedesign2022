using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            //other.gameObject.GetComponent<PlayerController>().LoseHealth();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


}

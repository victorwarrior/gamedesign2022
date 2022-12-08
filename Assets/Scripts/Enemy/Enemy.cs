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
            Invoke("RestartLevel", 1.5f);


            other.gameObject.GetComponent<PlayerController>().SetactiveFalse();
            other.gameObject.GetComponent<PlayerController>().otherPlayer.GetComponent<PlayerController>().SetactiveFalse();

            other.gameObject.GetComponent<PlayerController>().squashStrechAnimation.SetTrigger("Death");
            other.gameObject.GetComponent<PlayerController>().otherPlayer.GetComponent<PlayerController>().squashStrechAnimation.SetTrigger("Death");

            for (int i = 0; i < other.gameObject.transform.childCount; i++)
            {
                if (other.gameObject.transform.GetChild(i).gameObject.CompareTag("Eyes")) other.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            for (int i = 0; i < other.gameObject.GetComponent<PlayerController>().otherPlayer.transform.childCount; i++)
            {
                if (other.gameObject.GetComponent<PlayerController>().otherPlayer.transform.GetChild(i).gameObject.CompareTag("Eyes")) other.gameObject.GetComponent<PlayerController>().otherPlayer.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

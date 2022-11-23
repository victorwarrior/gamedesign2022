using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;

    public ParticleSystem Konfeti;

    public AudioClip confettiSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextScene);
            Konfeti.Play();
            gameObject.GetComponent<AudioSource>().PlayOneShot(confettiSound);
            Invoke("nextlevel", 1.5f);
        }
    }

    public void nextlevel()
    {
        SceneManager.LoadScene(nextScene);
    }



}

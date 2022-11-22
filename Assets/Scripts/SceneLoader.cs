using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;

    public ParticleSystem Konfeti;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Konfeti.Play();
            Invoke("nextlevel", 1.5f);
        }
    }

    public void nextlevel()
    {
        SceneManager.LoadScene(nextScene);
    }



}

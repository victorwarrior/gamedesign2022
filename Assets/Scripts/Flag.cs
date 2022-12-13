using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    public string nextScene;
    public string prevScene;

    public ParticleSystem Konfeti;


    public AudioClip confettiSound;

    void Update() {
        if (Input.GetKeyDown("o")) NextLevel();
        if (Input.GetKeyDown("i")) PrevLevel();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Konfeti.Play();
            gameObject.GetComponent<AudioSource>().PlayOneShot(confettiSound);
            Invoke("NextLevel", 1.5f);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void PrevLevel()
    {
        SceneManager.LoadScene(prevScene);        
    }



}

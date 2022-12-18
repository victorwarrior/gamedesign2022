using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public string prevScene;

    void Update()
    {
        if (Input.GetKeyDown("i")) PrevLevel();
    }

    private void PrevLevel()
    {
        SceneManager.LoadScene(prevScene);
    }
}

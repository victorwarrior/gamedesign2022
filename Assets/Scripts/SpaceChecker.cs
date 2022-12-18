using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceChecker : MonoBehaviour
{
    public string firstScene;

    void Update()
    {
        if (Input.GetKeyDown("space")) FirstLevel();
    }

    private void FirstLevel()
    {
        SceneManager.LoadScene(firstScene);
    }

}

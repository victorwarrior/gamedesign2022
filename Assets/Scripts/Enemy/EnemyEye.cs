/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEye : MonoBehaviour
{
   // public bool foundPlayer = false;

    public void Update()
    {

        //if (CurrentPlayer != null && foundPlayer == true)
        //    eyeFollow();
    }

    //public Vector3 CurrentPlayer { get; set; }

    //    public void eyeFollow(Vector3 playerPosition)

    public GameObject player;

    public void eyeFollow()
    {
        //if (CurrentPlayer == null) return;

        //var direction = new Vector2(
        //    CurrentPlayer.x - transform.position.x,
        //    CurrentPlayer.y - transform.position.y);

        //transform.up = direction;

        Vector3 playerPos = player.transform.position;

        Vector2 direction = new Vector2(
            playerPos.x - transform.position.x,
            playerPos.y - transform.position.y);

        transform.up = direction;
    }

}
*/
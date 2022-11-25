using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyEye : MonoBehaviour
{
    public string tagToDetect = "Player";
    public GameObject[] allPlayers;
    public GameObject closestPlayer;

    
    void Start()
    {
        allPlayers = GameObject.FindGameObjectsWithTag(tagToDetect);
    }

    // Update is called once per frame
    void Update()
    {
        closestPlayer = ClosestPlayer();
        print(closestPlayer.name);
        eyeFollow();
    }


    GameObject ClosestPlayer()
    {

        GameObject closestHere = gameObject;
        float leastDistance = Mathf.Infinity;

        foreach (var player in allPlayers)
        {

            float distanceHere = Vector3.Distance(transform.position, player.transform.position);

            if (distanceHere < leastDistance)
            {
                leastDistance = distanceHere;
                closestHere = player;
            }

        }
        return closestHere;
    }


    void eyeFollow()
    {
        Vector3 playerPos = closestPlayer.transform.position;

        Vector2 direction = new Vector2(
            playerPos.x - transform.position.x,
            playerPos.y - transform.position.y);

        transform.up = direction;
    }

}

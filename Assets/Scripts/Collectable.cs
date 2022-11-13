using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<PlayerController>().keys++;
        gameObject.SetActive(false);

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Door;
    public bool onButton = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Stone"))
        {
            Door.gameObject.GetComponent<DoorToLever>().stopUp();
            onButton = true;
        }
    }
    


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Door.gameObject.GetComponent<DoorToLever>().down();
            onButton = false;

        }
    }

}

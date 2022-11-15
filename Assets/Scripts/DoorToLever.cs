using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToLever : MonoBehaviour
{
    public float hastighedUp = 5f;
    public float hastighedDown;


    public float maxStop;
    public float minStop;

    public GameObject Lever;



    [HideInInspector] public void up()
    {
        transform.Translate((Vector2.up * hastighedUp) * Time.deltaTime, Space.World);
    }

    public void Start()
    {
        minStop = (float)gameObject.transform.position.y;
        maxStop = (float)gameObject.transform.position.y+ maxStop;
    }


    public void FixedUpdate() 
    {
        if (Lever.gameObject.GetComponent<Lever>().onButton == false && gameObject.transform.position.y > minStop)
        {
            down();
        }
    }

    [HideInInspector] public void down()
    {        
            transform.Translate(Vector2.down * hastighedDown * Time.deltaTime, Space.World);
    }

    public void stopUp()
    {
        if(maxStop >= gameObject.transform.position.y)
        {
            up();
        }
    }

    


}

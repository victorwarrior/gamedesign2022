using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool updateOn = false;
    public float Seconds = 5;

    void Start()
    {
        StartCoroutine(updateOff());
    }

    // Update is called once per frame
    void Update()
    {
        if (updateOn == true)
        {
            up();
        }

        if (updateOn == false)
        {
            down();
        }
    }

    void up()
    {
        transform.Translate(Vector2.up * Time.deltaTime, Space.World);
    }
    
    void down()
    {
        transform.Translate(Vector2.down * Time.deltaTime, Space.World);
    }

    IEnumerator updateOff()
    {
        yield return new WaitForSeconds(Seconds);
        updateOn = !updateOn;
        StartCoroutine(updateOff());    
    }
}

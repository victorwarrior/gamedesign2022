using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool updateOn = false;
    public bool opOgNed = false;
    public bool venstreOgHojre = false;
    public bool CircleFormation = false;
    public float Seconds = 5;
    public float hastighed;

    int cyklus = 0;


    void Start()
    {
        StartCoroutine(updateOff());
    }

    // Update is called once per frame
    void Update()
    {
        if (opOgNed == true)
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

        else
        {
            if (venstreOgHojre == true)
            {
                if (updateOn == true)
                {
                    left();
                }

                if (updateOn == false)
                {
                    right();
                }
            }
        }


        if (CircleFormation == true)
        {

            if (cyklus == 0)
            {
                left();
            }

            if (cyklus == 1)
            {
                up();
            }

            if (cyklus == 2)
            {
                right();
            }

            if (cyklus == 3)
            {
                down();
            }

        }
    }

        void up()
        {
            transform.Translate(Vector2.up * hastighed * Time.deltaTime, Space.World);
        }

        void down()
        {
            transform.Translate(Vector2.down * hastighed * Time.deltaTime, Space.World);
        }

        void left()
        {
            transform.Translate(Vector2.left * hastighed * Time.deltaTime, Space.World);
        }

        void right()
        {
            transform.Translate(Vector2.right * hastighed * Time.deltaTime, Space.World);
        }

        IEnumerator updateOff()
        {
            yield return new WaitForSeconds(Seconds);
            updateOn = !updateOn;
            cyklus = (cyklus + 1) % 4;
            StartCoroutine(updateOff());
        }
 }


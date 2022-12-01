using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public bool directionToggle  = false;
    public bool verMovement      = false;
    public bool horMovement      = false;
    public bool circularMovement = false;
    public float seconds         = 5;
    public float hastighed;

    int cyklus = 0;


    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (circularMovement == true) {
            if      (cyklus == 0) MoveLeft();
            else if (cyklus == 1) MoveUp();
            else if (cyklus == 2) MoveRight();
            else if (cyklus == 3) MoveDown();
        } else {
            if (verMovement        == true) {
                if (directionToggle == true)  MoveUp();
                if (directionToggle == false) MoveDown();
            }
            if (horMovement == true) {
                if (directionToggle == true)  MoveLeft();
                if (directionToggle == false) MoveRight();
            }
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector2.up * hastighed * Time.deltaTime, Space.World);
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * hastighed * Time.deltaTime, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * hastighed * Time.deltaTime, Space.World);
    }

    void MoveRight()
    {
        transform.Translate(Vector2.right * hastighed * Time.deltaTime, Space.World);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(seconds);
        directionToggle = !directionToggle;
        cyklus = (cyklus + 1) % 4;
        StartCoroutine(Timer());
    }
 }


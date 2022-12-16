using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingKeys : MonoBehaviour
{

    public int keyCount = 0;

    //key objects
    public GameObject keyYellow;
    public GameObject keyGreen;
    public GameObject keyBlue;

    //key positions which can be moved around in editor
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;

    //Floating effect and key target positions
    private Transform yellowTarget;
    private Transform greenTarget;
    private Transform blueTarget;
    public float floatSpeed;

    //start give keys target position variations
    //on pickup activate key in free target position
    //on use deactivate and set new target
    //when active do float



    private void Start()
    {
        keyYellow.transform.position = Pos1.position;
        keyGreen.transform.position = Pos2.position;
        keyBlue.transform.position = Pos3.position;

        yellowTarget = Pos1;
        greenTarget = Pos2;
        blueTarget = Pos3;

    }


    private void FixedUpdate()
    {
        //keyYellow.transform.position = yellowTarget.transform.position;
        //keyGreen.transform.position = greenTarget.transform.position;
        //keyBlue.transform.position = blueTarget.transform.position;


        if (keyYellow.activeSelf == true)
        {
            Float(keyYellow, yellowTarget);
        }

        if (keyGreen.activeSelf == true)
        {
            print("keygreen if");
            Float(keyGreen, greenTarget);
        }

        if (keyBlue.activeSelf == true)
        {
            Float(keyBlue, blueTarget);
        }


    }

    private void Float(GameObject key, Transform keyTarget)
    {
        print("key green float");
        if (Vector3.Distance(key.transform.position, keyTarget.position)>0.1f) //sorger for at floatspeed ikke nærmer sig 0 i det uendelige
        {
            floatSpeed = (Vector3.Distance(key.transform.position, keyTarget.position)) * 5;

        }
        else
        {
            floatSpeed = 0;
        }

        key.transform.position = Vector3.MoveTowards(key.transform.position, keyTarget.position, floatSpeed * Time.deltaTime);

    }

    public void ActivateFloatingKey(int keyType) //activates key in target position
    {
        keyCount++;

        switch (keyType)
        {
            case 1: //picked up yellow key
                yellowTarget = SetTargetPosition(yellowTarget, null);
                keyYellow.transform.position = yellowTarget.position;
                keyYellow.SetActive(true);
                break;

            case 2: //picked up green key
                greenTarget = SetTargetPosition(greenTarget, null);
                keyGreen.transform.position = greenTarget.position;
                keyGreen.SetActive(true);
                break;

            case 3: //picked up blue key
                blueTarget = SetTargetPosition(blueTarget, null);
                keyBlue.transform.position = blueTarget.position;
                keyBlue.SetActive(true);
                break;

        }

    }

    private Transform SetTargetPosition(Transform keyTarget, Transform deacKeyTarget) //decides and returns target position
    {

        if (deacKeyTarget == null) //der er ikke blevet deaktiveret nøgle
        {
            switch (keyCount)
            {
                case 3: //der er 3 aktive nøgler, hvoraf nøglen er nummer 3
                    keyTarget = Pos3;
                    break;
                case 2:
                    keyTarget = Pos2;
                    break;
                case 1:
                    keyTarget = Pos1;
                    break;
            }


        }
        else if (deacKeyTarget != null) //der er blevet deaktiveret nøgle
        {

            if (deacKeyTarget == Pos1) //tjekker hvilken position der nu er ledig
            {
                if (keyTarget == Pos2) //rykker target en position frem
                {
                    keyTarget = Pos1;
                }
                else if (keyTarget == Pos3)
                {
                    keyTarget = Pos2;
                }
            }
            else if (deacKeyTarget == Pos2)
            {
                if (keyTarget == Pos3)
                {
                    keyTarget = Pos2;
                }
                else if (keyTarget == Pos1)
                {
                    //no changes
                }
            }
            else if (deacKeyTarget == Pos3)
            {
                //no changes
            }

        }

        return keyTarget;

    }

    public void DeactivateFloatingKey(int keyType) //deactivates key and calls for new target positions
    {

        keyCount--;

        switch (keyType)
        {
            case 1: //used yellow key

                keyYellow.SetActive(false);
                greenTarget = SetTargetPosition(greenTarget, yellowTarget);
                blueTarget = SetTargetPosition(blueTarget, yellowTarget);
                break;

            case 2: //used green key

                keyGreen.SetActive(false);
                yellowTarget = SetTargetPosition(yellowTarget, greenTarget);
                blueTarget = SetTargetPosition(blueTarget, yellowTarget);
                break;

            case 3: //used blue key

                keyBlue.SetActive(false);
                yellowTarget = SetTargetPosition(yellowTarget, blueTarget);
                greenTarget = SetTargetPosition(greenTarget, blueTarget);
                break;

        }


    }



}










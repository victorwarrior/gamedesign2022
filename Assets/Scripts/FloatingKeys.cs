using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingKeys : MonoBehaviour
{

    public int keyCount = 0;

    //key objects
    public GameObject keyYellow;
    public GameObject keyGreen;
    public GameObject keyBlue;

    //intended key positions
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;


    private void Start()
    {
        keyYellow.transform.position = Pos1.position;
        keyGreen.transform.position = Pos2.position;
        keyBlue.transform.position = Pos3.position;

    }

    public void ActivateFloatingKey(Key key)
    {
        
        //keyColor = key.GetComponentInChildren<Renderer>().material.color;

        //activate right color in right position
        switch (key.keyType)
        {
            case 1:
                print("acFloatingKeys - case 1");
                ActRotateKeys(keyYellow, keyGreen, keyBlue);
                keyYellow.SetActive(true);
                break;

            case 2:
                ActRotateKeys(keyGreen, keyYellow, keyBlue);
                keyGreen.SetActive(true);
                break;

            case 3:
                ActRotateKeys(keyBlue, keyYellow, keyGreen);
                keyBlue.SetActive(true);
                break;
        }

        keyCount++;
    }

    public void DeactivateFloatingKey(int doorType)
    {
        switch (doorType)
        {
            case 1:
                keyYellow.SetActive(false);
                DeacRotateKeys(keyYellow, keyGreen, keyBlue);
                break;
            case 2:
                keyGreen.SetActive(false);
                DeacRotateKeys(keyGreen, keyYellow, keyBlue);
                break;
            case 3:
                keyBlue.SetActive(false);
                DeacRotateKeys(keyBlue, keyYellow, keyGreen);
                break;
        }

        keyCount--;
  
    }

    public void DeacRotateKeys (GameObject deacKey, GameObject otherKey1, GameObject otherKey2)
    {
        if (deacKey.transform.position == Pos1.position)
        {

            if (otherKey1.transform.position == Pos2.position)
            {
                otherKey1.transform.position = Pos1.position;
                otherKey2.transform.position = Pos2.position;
            }
            else
            {
                otherKey2.transform.position = Pos1.position;
                otherKey1.transform.position = Pos2.position;
            }
        }
        else if (deacKey.transform.position == Pos2.position)
        {
            if (otherKey1.transform.position == Pos1.position)
            {
                otherKey2.transform.position = Pos2.position;
            }
            else
            {
                otherKey1.transform.position = Pos2.position;
            }
        }

        deacKey.transform.position = Pos3.position;
    }

    public void ActRotateKeys(GameObject actionKey, GameObject otherKey1, GameObject otherKey2)
    {
        //nøgler må aldrig være på samme plads.
        //Tjek hvilken nøgle er blevet deaktiveret og send dem om i rækken

        if (actionKey.activeSelf == false) //hvis vi har deactiveret key
        {
            if (keyCount == 2)
            {
                actionKey.transform.position = Pos3.position;

            }
            else if (keyCount == 1)
            {

                actionKey.transform.position = Pos2.position;

                if (otherKey1.activeSelf == true)
                {
                    otherKey2.transform.position = Pos3.position;
                }
                else
                {
                    otherKey1.transform.position = Pos3.position;
                }

            }
            else if (keyCount == 0)
            {
                actionKey.transform.position = Pos1.position;
                otherKey1.transform.position = Pos2.position;
                otherKey2.transform.position = Pos3.position;

            }




        }


    }
}










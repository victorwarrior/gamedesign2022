using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingKeys : MonoBehaviour
{
    public int keyCount = 0;


    //deaktiver nøgler
    //ryk nøgler frem når de forsvinder
    //få dem til at floate

    //key objects
    public GameObject keyOne;
    public GameObject keyTwo;
    public GameObject keyThree;

    //intended key positions
    public Transform Pos1;
    public Transform Pos2;
    public Transform Pos3;

    //materials
    private Color keyColor;
    private Renderer[] floatingKeyRenderers;

    public void ActivateFloatingKey(Key key)
    {
        keyCount++;
        keyColor = key.GetComponentInChildren<Renderer>().material.color;

        switch (key.keyType)
        {
            case 1:
                print("case 1");       
                SetColor(keyOne);
                keyOne.SetActive(true);
                break;

            case 2:
                print("case 2");
                SetColor(keyTwo);
                keyTwo.SetActive(true);
                break;

            case 3:
                print("case 3");
                SetColor(keyThree);
                keyThree.SetActive(true);
                break;
        }
        print("end of switch");
        
    }

    public void SetColor(GameObject floatKey)
    {
        floatingKeyRenderers = floatKey.GetComponentsInChildren<Renderer>();
        print(floatingKeyRenderers.Length);

        foreach (Renderer renderer in floatingKeyRenderers)
        {
            renderer.material.SetColor("_Color", keyColor);
        }
    }

    public void RotateKeys()
    {

    }

    public void DeactivateFloatingKey(Door door)
    {
        switch (door.keyType)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;

        }
    }








}

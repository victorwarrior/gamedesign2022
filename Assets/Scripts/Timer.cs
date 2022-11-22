using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Image uiFill;

    private float Duration;
    private float remainingDuration;

    private void Start()
    {
        Duration = GameObject.FindGameObjectWithTag("TimerDoor").GetComponent<DoorToLever>().maxStop - GameObject.FindGameObjectWithTag("TimerDoor").GetComponent<DoorToLever>().minStop;
        print("duration: "+Duration);
    }


    //ved start har døren en start og en slut position
    //en down funktion som "tæller ned"


}

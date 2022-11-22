using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorToLever : MonoBehaviour
{
    public float hastighedUp = 5f;
    public float hastighedDown;
    public float maxStop;
    public float minStop;
    public GameObject Lever;
    public GameObject timer;

    private float timerDuration;
    private float timerRemainingDuration;
    private Image uiFill;

    [HideInInspector]
    public void up()
    {
        transform.Translate((Vector2.up * hastighedUp) * Time.deltaTime, Space.World);
    }

    public void Start()
    {
        minStop = (float)gameObject.transform.position.y;
        maxStop = (float)gameObject.transform.position.y + maxStop;

        timerDuration = maxStop - (minStop);
        uiFill = timer.GetComponent<Image>();

        StartCoroutine(UpdateTimer());
    }

    public void FixedUpdate()
    {
        timerRemainingDuration = transform.position.y - minStop;

        if (Lever.gameObject.GetComponent<Lever>().onButton == false && gameObject.transform.position.y > minStop)
        {
            down();
        }


        //print("duration:"+timerDuration+" remaining:"+timerRemainingDuration+" lerp:"+Mathf.InverseLerp(0, timerDuration, timerRemainingDuration));
    }

    [HideInInspector]
    public void down()
    {
        transform.Translate(Vector2.down * hastighedDown * Time.deltaTime, Space.World);

    }

    public void stopUp() //når player står på knappen
    {
        if (maxStop >= gameObject.transform.position.y)
        {
            up();
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (timerDuration >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, timerDuration, timerRemainingDuration);
            yield return null;
        }
        yield return new WaitForFixedUpdate();
    }


}

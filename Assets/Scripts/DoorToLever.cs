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
    public GameObject timerBackground;

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

        timerDuration = maxStop - minStop;
        uiFill = timer.GetComponent<Image>();
        
    }

    public void FixedUpdate()
    {
        timerRemainingDuration = transform.position.y - minStop;

        if (Lever.gameObject.GetComponent<Lever>().onButton == false && gameObject.transform.position.y > minStop)
        {
            down();
        }

        if (timerRemainingDuration < 0)
        {
            timerBackground.GetComponent<Image>().enabled = false;
            uiFill.enabled = false;
        }
        //print("duration:" + timerDuration + " remaining:" + timerRemainingDuration + " lerp:" + Mathf.InverseLerp(0, timerDuration, timerRemainingDuration));
    }

    [HideInInspector]
    public void down()
    {
        transform.Translate(Vector2.down * hastighedDown * Time.deltaTime, Space.World);
    }

    public void stopUp() //når player står på knappen
    {
        StartCoroutine(UpdateTimer());
        timerBackground.GetComponent<Image>().enabled = true;
        uiFill.enabled = true;

        if (maxStop >= gameObject.transform.position.y)
        {
            up();
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (timerRemainingDuration >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, timerDuration, timerRemainingDuration);
            //a- start of range, b- end of range, value- the point within the range you want to calculate
            yield return null;
        }
        yield return new WaitForFixedUpdate();
    }

}

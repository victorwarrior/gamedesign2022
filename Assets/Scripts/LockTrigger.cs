using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTrigger : MonoBehaviour
{
    public GameObject triggerObject;
    public string keyword;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //foreach (string element in collision.gameObject.GetComponent<KeyHolder>().keys)
            //{
            //    if (element == keyword)
            //    {
            //        collision.gameObject.GetComponent<KeyHolder>().keys.Remove(element);
            //        triggerObject.gameObject.SetActive(true);
            //        gameObject.SetActive(false);
            //    }
            //}
            //foreach har problemer med List<T>.Remove()

            for (int i = 0; i < collision.gameObject.GetComponent<KeyHolder>().keys.Count; i++)
            {
                if (keyword == collision.gameObject.GetComponent<KeyHolder>().keys[i])
                {
                    collision.gameObject.GetComponent<KeyHolder>().keys.RemoveAt(i);

                    if (triggerObject.gameObject != null)
                    {
                        triggerObject.gameObject.SetActive(true);
                    }
                    
                    gameObject.SetActive(false);
                }
            }

        }
    }
}


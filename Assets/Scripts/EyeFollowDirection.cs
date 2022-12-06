using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowDirection : MonoBehaviour {

    public GameObject player;
    float x = 1;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
        if (player.GetComponent<PlayerController>().direction != 0) {
            //for (int i = 0; i < transform.childCount; i++) {
            //    if (transform.GetChild(i).gameObject.CompareTag("SpecialEye")) transform.GetChild(i).gameObject.SetActive(false);
            //}
            if (player.GetComponent<PlayerController>().direction == -1) x = -1;
            if (player.GetComponent<PlayerController>().direction ==  1) x =  1;
            Vector2 direction = new Vector2(x, 0);
            transform.up = direction;
        } else {
            //for (int i = 0; i < transform.childCount; i++) {
            //    if (transform.GetChild(i).gameObject.CompareTag("SpecialEye")) transform.GetChild(i).gameObject.SetActive(true);
            //}

        }


    }
}

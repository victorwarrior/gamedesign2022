using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingLineRenderer : MonoBehaviour
{
    

    LineRenderer ropeLineRenderer;

    public GameObject otherPlayer;

    void Start()
    {
        ropeLineRenderer = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        ropeLineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z+0.5f));
        ropeLineRenderer.SetPosition(1, new Vector3(otherPlayer.transform.position.x - 0.5f, otherPlayer.transform.position.y, otherPlayer.transform.position.z+0.5f));
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitter : MonoBehaviour
{
    public ParticleSystem glitter;

    private void FixedUpdate()
    {
        glitter.Play();
    }
}

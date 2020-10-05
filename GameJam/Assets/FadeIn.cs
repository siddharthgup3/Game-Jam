using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    private AudioSource source;

    private void Start()
    {
        source = transform.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {

        if (source.volume < 0.3f)
        {
            source.volume += 0.1f * Time.deltaTime;
        }
    }
}
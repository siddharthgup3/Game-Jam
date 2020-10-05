using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveJump : MonoBehaviour
{
    JPPlayerMove playerScript;
    private AudioSource aSrc;


    private void Start()
    {
        aSrc = GetComponentInParent<AudioSource>();
        playerScript = FindObjectOfType<JPPlayerMove>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            aSrc.Play();
            playerScript.GiveSingleJump();
            Destroy(gameObject);
        }
    }
}
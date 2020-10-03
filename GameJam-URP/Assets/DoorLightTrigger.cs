using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightTrigger : MonoBehaviour
{

    public GameObject doorLight1;
    public GameObject doorLight2;

    public Material whiteAmbient;
    private AudioSource aSrc;



    // Start is called before the first frame update
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aSrc.Play();
            doorLight1.GetComponent<Renderer>().material = whiteAmbient;
            doorLight2.GetComponent<Renderer>().material = whiteAmbient;

            gameObject.transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

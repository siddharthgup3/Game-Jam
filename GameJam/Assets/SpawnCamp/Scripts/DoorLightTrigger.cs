using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLightTrigger : MonoBehaviour
{

    public GameObject doorLight1;
    public GameObject doorLight2;

    public GameObject startingPortalEffect;
    public GameObject endingPortalEffect;

    public Material whiteAmbient;


    public bool needsToDisappear;


   // private AudioSource aSrc;



    // Start is called before the first frame update
    void Start()
    {
        //aSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //  aSrc.Play();

            if (needsToDisappear)
            {
                startingPortalEffect.SetActive(false);
                endingPortalEffect.SetActive(false);
            }
            else
            {
                startingPortalEffect.SetActive(false);
                endingPortalEffect.SetActive(true);
            }

            doorLight1.GetComponent<Renderer>().material = whiteAmbient;
            doorLight2.GetComponent<Renderer>().material = whiteAmbient;

            gameObject.transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

    public AudioClip portalSound;
    public GameObject otherPortal;
    public PortalCounter portalCounter;

    private void OnTriggerEnter(Collider other)
    {
        BlackFader.FadeFromBlack();

        if(portalCounter != null)
        {
            portalCounter.portalCount++;
        }
        else {

            Debug.Log("No Portal Counter Was Found In Scene.");
        
        }

        if (other.CompareTag("Player")) {
            other.GetComponentInChildren<JPPlayerCam>().enabled = false;
            //playerCam.enabled = false;

            Vector3 v = other.transform.rotation.eulerAngles;
            Vector3 y = otherPortal.transform.rotation.eulerAngles;

            other.transform.position = otherPortal.transform.position;
            other.transform.rotation = Quaternion.Euler(v.x, y.y, v.z);
            other.GetComponentInChildren<JPPlayerCam>().enabled = true;

            SoundController.instance.PlayClip(portalSound);

            gameObject.SetActive(false);
        }
    }
}

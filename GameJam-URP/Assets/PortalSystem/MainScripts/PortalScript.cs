using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

    public GameObject otherPortal;

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {
            other.GetComponentInChildren<JPPlayerCam>().enabled = false;
            //playerCam.enabled = false;

            Vector3 v = other.transform.rotation.eulerAngles;
            Vector3 y = otherPortal.transform.rotation.eulerAngles;

            other.transform.position = otherPortal.transform.position;
            other.transform.rotation = Quaternion.Euler(v.x, y.y, v.z);
            other.GetComponentInChildren<JPPlayerCam>().enabled = true;
        }
    }
}

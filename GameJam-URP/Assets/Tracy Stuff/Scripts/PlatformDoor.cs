using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDoor : MonoBehaviour
{
    public GameObject[] platforms;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            foreach(GameObject platform in platforms)
            {
                platform.GetComponent<FadeInMat>().enabled = true;
                platform.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}

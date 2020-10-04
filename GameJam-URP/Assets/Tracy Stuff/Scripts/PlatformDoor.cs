using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDoor : MonoBehaviour
{
    GameObject[] redPlatforms;
    GameObject[] purplePlatforms;
    public enum GhostType { red, purple}
    public int platformType;
    private void Start()
    {
        redPlatforms = GameObject.FindGameObjectsWithTag("RedPlatform");
        purplePlatforms = GameObject.FindGameObjectsWithTag("PurplePlatform");
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && platformType == 0)
        {
            foreach(GameObject platform in redPlatforms)
            {
                platform.GetComponent<BoxCollider>().enabled = true;
                platform.GetComponent<ParticleSystem>().Play();
            }
            foreach (GameObject platform in purplePlatforms)
            {
                platform.GetComponent<BoxCollider>().enabled = false;
                platform.GetComponent<ParticleSystem>().Stop();
            }

        }
        else if(col.gameObject.CompareTag("Player"))
        {
            foreach(GameObject platform in redPlatforms)
            {
                platform.GetComponent<BoxCollider>().enabled = false;
                platform.GetComponent<ParticleSystem>().Stop();
            }
            foreach (GameObject platform in purplePlatforms)
            {
                platform.GetComponent<BoxCollider>().enabled = true;
                platform.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}

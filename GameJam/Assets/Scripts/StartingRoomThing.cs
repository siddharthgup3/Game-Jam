using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoomThing : MonoBehaviour {

    public GameObject door;
    public GameObject blocker;

    public AudioClip introClip;
    public AudioClip doorOpen;

    private void Start() {
        StartCoroutine(LiftDoorTimer());
        SoundController.instance.PlayClip(introClip);
    }

    private void OnTriggerEnter(Collider other) {
        blocker.SetActive(true);
    }

    IEnumerator LiftDoorTimer() {
        yield return new WaitForSeconds(10f);
        SoundController.instance.PlayClip(doorOpen);
        LeanTween.moveY(door, 5.84f, 4f);
    }
}

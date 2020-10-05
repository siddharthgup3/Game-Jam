using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

    public bool playMultiple;
    public AudioClip clipToPlay;
    public List<AudioClip> extraClips = new List<AudioClip>();

    private bool played;

    private void OnTriggerEnter(Collider other) {
        SoundController soundController = other.GetComponent<SoundController>();

        if (soundController) {

            if (played == true) {

                if (playMultiple == true) {
                    soundController.PlayClip(extraClips[Random.Range(0, extraClips.Count)]);
                }

            } else {
                soundController.PlayClip(clipToPlay);
                played = true;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController instance;
    public AudioSource source;

    private void Awake() {

        if (instance != null) {
            Destroy(this);

        } else {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip) {
        source.PlayOneShot(clip);
    }
}

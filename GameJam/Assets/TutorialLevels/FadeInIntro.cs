using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInIntro : MonoBehaviour {

    private AudioSource source;
    private bool fading;

    private void Start() {
        source = transform.GetComponent<AudioSource>();
        StartCoroutine(Wait());
    }

    private void FixedUpdate() {

        if (fading == true) {
            source.volume += 0.1f * Time.deltaTime;

            if (source.volume >= 0.3f) {
                fading = false;
            }
        }
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(8);
        fading = true;
    }
}

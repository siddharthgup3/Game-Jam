using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGhostPlat : MonoBehaviour {

    public List<GhostPlatform> ghostPlatforms;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {

            for (int i = 0; i < ghostPlatforms.Count; i++) {
                ghostPlatforms[i].Solidify();
            }
        }
    }
}

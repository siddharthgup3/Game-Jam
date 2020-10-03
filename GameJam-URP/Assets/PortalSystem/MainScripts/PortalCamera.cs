using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

    public Transform exitPortal;
    public Transform entrancePortal;
    [HideInInspector] public Transform playerCamera;

    private void Start()
    {
        playerCamera = Camera.main.transform;
    }

    private void Update() {
        // Vector3 playerOffsetFromPortal = playerCamera.position - entrancePortal.position;
        // transform.position = exitPortal.position + playerOffsetFromPortal;
        //
        // float angularDifferenceBetweenPortalRotations = Quaternion.Angle(exitPortal.rotation, entrancePortal.rotation);
        //
        // Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        // Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        // transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

        var m = exitPortal.localToWorldMatrix * entrancePortal.worldToLocalMatrix * playerCamera.localToWorldMatrix;
        this.transform.rotation = m.rotation;
        
        if(Input.GetKeyDown(KeyCode.R))
            RestartMechanic.RestartGame();
        
    }
}

using UnityEngine;

public class JPCamZoom : MonoBehaviour
{
    Camera playerCam;
    public Camera gunCam;


    private float desiredFOV;
    private float zoomSpeed = 10f;

    public JPPlayerMove move;

    void Start()
    {
        playerCam = GetComponent<Camera>();
    }

    void Update()
    {

        if (move.isRunning && !move.isCrouching)
        {
            desiredFOV = 58f;
        }
        else
        {
            if (Input.GetButton("Fire2"))
            {
                desiredFOV = 45f;
            }

            else
            {
                desiredFOV = 60f;
            }
        }


        playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, desiredFOV, zoomSpeed * Time.deltaTime);
       // gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, desiredFOV, zoomSpeed * Time.deltaTime);

    }
}

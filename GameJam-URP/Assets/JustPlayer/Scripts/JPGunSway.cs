using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPGunSway : MonoBehaviour
{
    public float amt;
    public float smoothAmt;

    public float maxAmnt;

    Vector3 intPos;

    void Start()
    {
        intPos = transform.localPosition;
    }

    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * amt;
        float moveY = -Input.GetAxis("Mouse Y") * amt;

        moveX = Mathf.Clamp(moveX, -maxAmnt, maxAmnt);
        moveY = Mathf.Clamp(moveY, -maxAmnt, maxAmnt);


        Vector3 finalPos = new Vector3(moveX, moveY, 0f);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos + intPos, Time.deltaTime * smoothAmt);
    }
}

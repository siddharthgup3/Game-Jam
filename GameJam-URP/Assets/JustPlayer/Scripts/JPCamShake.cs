using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPCamShake : MonoBehaviour
{
    public float shake = 0f;
    public float shakeAmount = .55f;
    public float shakeTimer = 3f;

    // Update is called once per frame
    void Update()
    {
        if (shake < 0f)
        {
            shake = 0f;
        }

        if (shake > 0f)
        {
            transform.localRotation = Quaternion.Euler(Random.insideUnitSphere.x * shakeAmount, 0f, 0f);
            shake -= Time.deltaTime * shakeTimer;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void ShakeCamera()
    {
        shake = .2f;
    }

    public void MiniExplodeCamera()
    {
        shake = 1.25f;
    }

    public void ExplodeCamera()
    {
        shake = 2f;
    }
}
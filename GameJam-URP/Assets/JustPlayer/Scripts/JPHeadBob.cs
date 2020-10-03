using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPHeadBob : MonoBehaviour
{
    //JPPlayerMove playerMove;

    //private float timer = 0.0f;
    //public float midpoint = 0.0f;

    //public float bobbingSpeed;
    //public float bobbingAmount;

    //public float normalBobbingSpeed = 0.25f;
    //public float normalBobbingAmount = 0.03f;

    //public float runningBobbingSpeed = 0.25f;
    //public float runningBobbingAmount = 0.03f;

    //public float count = 0;
    //float waveslice;
    //float horizontal;
    //float vertical;
    //Vector3 cSharpConversion;

    //private void Start()
    //{
    //    playerMove = GetComponentInParent<JPPlayerMove>();
    //}

    //void Update()
    //{
    //    GetInput();
    //    GetSpeed();
    //    UseSpeedToBob();
    //}

    //void GetInput()
    //{
    //    horizontal = Input.GetAxis("Horizontal");
    //    vertical = Input.GetAxis("Vertical");
    //}

    //void GetSpeed()
    //{
    //    if (playerMove.myCC.isGrounded && !playerMove.isCrouching)
    //    {
    //        if (playerMove.isRunning && !playerMove.isAiming)
    //        {
    //            bobbingSpeed = runningBobbingSpeed;
    //            bobbingAmount = runningBobbingAmount;
    //        }
    //        else
    //        {
    //            bobbingSpeed = normalBobbingSpeed;
    //            bobbingAmount = normalBobbingAmount;
    //        }
    //    }
    //    else
    //    {
    //        bobbingSpeed = 0;
    //        bobbingAmount = 0;
    //    }
    //}

    //void UseSpeedToBob()
    //{
    //    waveslice = 0.0f;

    //    cSharpConversion = transform.localPosition;

    //    if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
    //    {
    //        timer = 0.0f;
    //    }
    //    else
    //    {
    //        waveslice = Mathf.Sin(timer);
    //        timer = timer + bobbingSpeed;
    //        if (timer > Mathf.PI * 2)
    //        {
    //            timer = timer - (Mathf.PI * 2);
    //        }
    //    }
    //    if (waveslice != 0)
    //    {
    //        float translateChange = waveslice * bobbingAmount;
    //        float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
    //        totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
    //        translateChange = totalAxes * translateChange;
    //        cSharpConversion.y = midpoint + translateChange;
    //    }
    //    else
    //    {
    //        cSharpConversion.y = midpoint;
    //    }
    //    transform.localPosition = cSharpConversion;
    //}
}

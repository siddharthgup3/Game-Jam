using System.Collections;
using UnityEngine;

public class JPFootStep : MonoBehaviour
{

    //float stepRate;
    //public float runStepRate = .31f;
    //public float walkStepRate = .42f;

    //bool hasSlid;
    //float stepCoolDown;

    //public AudioClip footStep;
    //public AudioClip slideEffect;

    //AudioSource aSrc;
    //JPPlayerMove playerMove;

    //private void Start()
    //{
    //    aSrc = GetComponent<AudioSource>();
    //    playerMove = GetComponentInParent<JPPlayerMove>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    stepCoolDown -= Time.deltaTime;

    //    if ((Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f) && stepCoolDown < 0f && playerMove.myCC.isGrounded && !playerMove.isCrouching && !playerMove.isDashing)
    //    {
    //        if (playerMove.isRunning && !playerMove.isAiming)
    //        {
    //            aSrc.pitch = .8f + Random.Range(0f, 0.1f);
    //            aSrc.PlayOneShot(footStep);

    //            stepCoolDown = runStepRate;
    //        }
    //        else
    //        {
    //            aSrc.pitch = .8f + Random.Range(0f, 0.1f);
    //            aSrc.PlayOneShot(footStep);

    //            stepCoolDown = walkStepRate;
    //        }
    //    }

    //    if (Input.GetKey(KeyCode.LeftControl) && playerMove.isRunning && playerMove.myCC.isGrounded)
    //    {
    //        if (!hasSlid)
    //        {
    //            aSrc.PlayOneShot(slideEffect);
    //            hasSlid = true;
    //        }
    //    }

    //    if (Input.GetKeyUp(KeyCode.LeftControl))
    //    {
    //        aSrc.Stop();
    //        hasSlid = false;
    //    }
    //}
}

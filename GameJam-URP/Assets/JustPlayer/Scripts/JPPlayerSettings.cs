using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPPlayerSettings : MonoBehaviour
{
    [Header("Player Constant Floats")]
    public float walkSpeed = 5.0f;
    public float runSpeed = 8.0f;
    public float crouchSpeed = 2.0f;
    public float jumpForce = 10.0f;
    public float dashForce = 15.0f;
    public float airSpeed = 3.5f;
    public float climbSpeed = 4.5f;
    public float slideSlowSpeed = 1.5f;
    public float momentumDampingSpeed = 0.5f;
    public float groundMomentumDampingSpeed = 10.0f;
    public float groundDashStaminaCost = 33.0f;
    public float airDashStaminaCost = 66.0f;
    public float sprintStaminaCost = 30.0f; //???
    public float allowedJumps = 2.0f;

    [Header("Players Dedicated Physics Settings")]
    public float mass = 1.0f;
    public float jumpSmoothing = 7.0f;
    public float dashSmoothing = 7.0f;
    public float realGravity = -10.0f;
}

using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;

[RequireComponent(typeof(CharacterController))]

public class JPPlayerMove : MonoBehaviour
{
    [Header("Players Dedicated Physics Settings")]
    public float mass = 1.0f;
    public float jumpSmoothing = 7.0f;
    public float dashSmoothing = 7.0f;
    public float realGravity = -10.0f;

    #region Serialized Variables
    [Header("Player Dynamic Floats")]
    [SerializeField] public float generatedGravity;
    [SerializeField] public float jumpCount = 2f;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float modifiedSpeed;
    [SerializeField] public float playersActualSpeed;

    [Header("Player Dynamic Vectors")]
    [SerializeField] private Vector3 groundedInput;
    [SerializeField] private Vector3 airbourneInput;
    [SerializeField] private Vector3 jump;
    [SerializeField] private Vector3 dash;
    [SerializeField] public Vector3 finalVelocity;
    [SerializeField] private Vector3 playersLastPosition;

    [Header("Player Dynamic Bools")]
    [SerializeField] public bool isAiming;
    [SerializeField] public bool isCrouching;
    [SerializeField] public bool isRunning;
    [SerializeField] public bool isSliding;
    [SerializeField] public bool isDashing;
    [SerializeField] public bool obstacleOverhead;
    [SerializeField] public bool isGrounded;
    [SerializeField] public bool isClimbing;
    [SerializeField] public bool isInAir;

    [Header("Variables Assigned Via Script")]
    [SerializeField] private float jumpMagnitude = 0.25f;
    [SerializeField] private float dashMagnitude = 0.25f;
    [SerializeField] private Vector3 standingVector = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 crouchingVector = new Vector3(0, 0.5f, 0);

    public float jumpForce = 10.0f;
    public float dashForce = 15.0f;
    public float airSpeed = 3.5f;
    public float momentumDampingSpeed = 0.5f;
    public float groundMomentumDampingSpeed = 10.0f;
    public float walkSpeed = 5.0f;
    public float allowedJumps = 2.0f;

    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private CharacterController myCC;

    [Header("Variables Assigned Via Editor")]
    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip landSound;
    public Camera myCam;
    #endregion

    void Awake()
    {
        // Get Components

        audioSrc = GetComponent<AudioSource>();
        myCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundCheck();
        CrouchCheck();
        Jump();
        DashCheck();
        Move();
    }

    private void Move()
    {
        GetInput();
        ApplyGravity();

        finalVelocity =
            (groundedInput * walkSpeed) +
            (airbourneInput * airSpeed) +
            (Vector3.up * generatedGravity);

        if (dash.magnitude > dashMagnitude)
        {
            finalVelocity += dash;
            StartCoroutine(Dash());
        }

        if (jump.magnitude > jumpMagnitude)
        {
            finalVelocity += jump;
        }
        myCC.Move(finalVelocity * Time.deltaTime);

        jump = Vector3.Lerp(jump, Vector3.zero, jumpSmoothing * Time.deltaTime);
        dash = Vector3.Lerp(dash, Vector3.zero, dashSmoothing * Time.deltaTime);
    }

    private void GetInput()
    {
        if (!myCC.isGrounded) // Air Input
        {
            airbourneInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            airbourneInput.Normalize();
            airbourneInput = transform.TransformDirection(airbourneInput);

            groundedInput = Vector3.Lerp(groundedInput, Vector3.zero, momentumDampingSpeed * Time.deltaTime);
        }
        else // Grounded Input
        {
            airbourneInput = Vector3.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                groundedInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                groundedInput.Normalize();
                groundedInput = transform.TransformDirection(groundedInput);
            }
            else
                groundedInput = Vector3.Lerp(groundedInput, Vector3.zero, groundMomentumDampingSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (myCC.isGrounded && generatedGravity < realGravity)
            generatedGravity = realGravity;

        generatedGravity += realGravity * Time.deltaTime;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0) //&& myCC.isGrounded
        {
            ResetVerticalVelocity();

            // add jump here
            jump += Vector3.up.normalized * (jumpForce) / mass;
            jumpCount--;
        }
    }

    private void DashCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!myCC.isGrounded && Input.GetKey(KeyCode.W))
            {
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += Camera.main.transform.forward * (dashForce * 1f);
            }
            else if (!myCC.isGrounded && Input.GetKey(KeyCode.S))
            {
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += -Camera.main.transform.forward * (dashForce * 1f);
            }
            else if (!myCC.isGrounded && Input.GetKey(KeyCode.A))
            {
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += -Camera.main.transform.right * (dashForce * 1.8f);
            }
            else if (!myCC.isGrounded && Input.GetKey(KeyCode.D))
            {
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += Camera.main.transform.right * (dashForce * 1.8f);
            }

            else if (myCC.isGrounded) // is Grounded
            {
                if (Input.GetKey(KeyCode.W))
                {
                    audioSrc.PlayOneShot(dashSound);
                    dash += transform.forward * dashForce;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    audioSrc.PlayOneShot(dashSound);
                    dash += -transform.forward * dashForce;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    audioSrc.PlayOneShot(dashSound);
                    dash += -transform.right * dashForce;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    audioSrc.PlayOneShot(dashSound);
                    dash += transform.right * dashForce;
                }
            }
        }
    }

    private void CrouchCheck()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            // Height and Center deltaMaxs need to match
            myCC.height = Mathf.MoveTowards(myCC.height, 1f, (7f * Time.deltaTime));
            myCC.center = Vector3.MoveTowards(myCC.center, crouchingVector, (7f * Time.deltaTime));
        }
        else if(!obstacleOverhead)
        {
            isCrouching = false;
            // Height and Center deltaMaxs need to match
            myCC.height = Mathf.MoveTowards(myCC.height, 2f, (5f * Time.deltaTime));
            myCC.center = Vector3.MoveTowards(myCC.center, standingVector, (5f * Time.deltaTime));
        }
    }

    private void GroundCheck()
    {
        if (myCC.isGrounded)
        {
            jumpCount = allowedJumps;
        }
    }

    public void ResetVerticalVelocity()
    {
        jump.y = 0f;
        generatedGravity = 0f;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(.3f);
        isDashing = false;
    }
}
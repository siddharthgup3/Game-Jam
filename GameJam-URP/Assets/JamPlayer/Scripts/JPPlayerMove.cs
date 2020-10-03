using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class JPPlayerMove : MonoBehaviour
{
    #region Vars
    [Header("EnableUnlimitedJump")]
    public bool unlimitedDebugJumps;

    private bool hasSingleJump;
    private bool hasDoubleJump;
    private int doubleJumpCount;
    private bool isDoubleJumping;

    private float mass = 1.0f;
    private float jumpSmoothing = 7.0f;
    private float dashSmoothing = 7.0f;
    public float realGravity = -10.0f;

    private float generatedGravity;

    private Vector3 groundedInput;
    private Vector3 airbourneInput;
    private Vector3 jump;
    private Vector3 dash;
    private Vector3 finalVelocity;

    private float jumpMagnitude = 0.25f;
    private float dashMagnitude = 0.25f;
    private Vector3 standingVector = new Vector3(0, 0, 0);

    public float jumpForce = 10.0f;
    private float dashForce = 15.0f;
    private float airSpeed = 3.5f;
    private float momentumDampingSpeed = 0.5f;
    private float groundMomentumDampingSpeed = 10.0f;
    private float walkSpeed = 5.0f;

    private AudioSource audioSrc;
    private CharacterController myCC;
    private Camera myCam;

    [Header("Sound Clips")]
    public AudioClip jumpSound;
    public AudioClip dashSound;
    #endregion

    void Awake()
    {
        // Get Components
        myCam = Camera.main;
        audioSrc = GetComponent<AudioSource>();
        myCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        GroundCheck();
        BrokenAssCrouchMethod();  // if you remove (cc and/or portals break)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hasSingleJump)
            {
                ResetVerticalVelocity();

                // add jump here
                jump += Vector3.up.normalized * (jumpForce) / mass;
                hasSingleJump = false;
            }

            if (hasDoubleJump && doubleJumpCount > 0)
            {
                isDoubleJumping = true;
                ResetVerticalVelocity();

                // add jump here
                jump += Vector3.up.normalized * (jumpForce) / mass;
                doubleJumpCount--;
            }

            if (unlimitedDebugJumps)
            {
                ResetVerticalVelocity();

                // add jump here
                jump += Vector3.up.normalized * (jumpForce) / mass;
            }
        }
    }

    [ContextMenu("Add Single Jump")]
    public void GiveSingleJump()
    {
        hasSingleJump = true;
    }

    [ContextMenu("Add Double Jump")]
    public void GiveDoubleJump()
    {
        hasDoubleJump = true;
        doubleJumpCount = 2;
    }

    private void DashCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //if (!myCC.isGrounded && Input.GetKey(KeyCode.W))
            //{
            //    ResetVerticalVelocity();
            //    audioSrc.PlayOneShot(dashSound);
            //    dash += Camera.main.transform.forward * (dashForce * 1f);
            //}
            //else if (!myCC.isGrounded && Input.GetKey(KeyCode.S))
            //{
            //    ResetVerticalVelocity();
            //    audioSrc.PlayOneShot(dashSound);
            //    dash += -Camera.main.transform.forward * (dashForce * 1f);
            //}
            //else if (!myCC.isGrounded && Input.GetKey(KeyCode.A))
            //{
            //    ResetVerticalVelocity();
            //    audioSrc.PlayOneShot(dashSound);
            //    dash += -Camera.main.transform.right * (dashForce * 1.8f);
            //}
            //else if (!myCC.isGrounded && Input.GetKey(KeyCode.D))
            //{
            //    ResetVerticalVelocity();
            //    audioSrc.PlayOneShot(dashSound);
            //    dash += Camera.main.transform.right * (dashForce * 1.8f);
            //}

            if (myCC.isGrounded) // is Grounded
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

    private void BrokenAssCrouchMethod()
    {
       myCC.center = standingVector;
    }

    private void GroundCheck()
    {
        if (isDoubleJumping && myCC.isGrounded)
        {
            hasDoubleJump = false;
            doubleJumpCount = 0;
        }
    }

    public void ResetVerticalVelocity()
    {
        jump.y = 0f;
        generatedGravity = 0f;
    }
}
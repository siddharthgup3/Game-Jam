using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(JPPlayerSettings))]

public class JPPlayerMove : MonoBehaviour
{
    #region Serialized Variables
    [Header("Player Dynamic Floats")]
    [SerializeField] public float generatedGravity;
    [SerializeField] public float jumpCount = 2f;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float modifiedSpeed;
    [SerializeField] public float playersActualSpeed;
    [SerializeField] private float jumpModifier;

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
    [SerializeField] private float wallRaycastDist = 1f;
    [SerializeField] private Vector3 wallRaycastOffset = new Vector3(0f, -0.8f, 0f);
    [SerializeField] private float ceilingCheckRayCastDist = .5f;
    [SerializeField] private Vector3 ceilingCheckRayCastOffset = new Vector3(0f, .5f, 0f);

    [SerializeField] private JPPlayerSettings playerSettings;
    [SerializeField] private AudioSource audioSrc;
    [SerializeField] private CharacterController myCC;
    [SerializeField] public JPCamShake camShake;
    [SerializeField] private Stamina playerStamina;
    [SerializeField] private SprintStamina playerSprintStamina;

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
        playerSettings = GetComponent<JPPlayerSettings>();
        playerStamina = GetComponent<Stamina>();
        playerSprintStamina = GetComponent<SprintStamina>();
        playersLastPosition = transform.position;
    }

    void Update()
    {
        isGrounded = myCC.isGrounded;
        if (!isGrounded)
        {
            isInAir = true;
        }
        else
        {
            isInAir = false;
        }

        if (!isClimbing)
        {
            GroundCheck();
            CeilingCheck();
            CrouchCheck();
            JumpAndClimbCheck();
            DashCheck();
            Move();
        }
        else
            JumpAndClimbCheck();

        GetPlayersSpeed();

        if (isRunning && !isCrouching)
        {
            playerSprintStamina.playerSprintStamina -= playerSettings.sprintStaminaCost * Time.deltaTime;
        }
    }

    private void Move()
    {
        GetInput();
        ApplyGravity();
        GetPlayersMoveSpeed();

        finalVelocity =
            (groundedInput * initialSpeed) +
            (airbourneInput * playerSettings.airSpeed) +
            (Vector3.up * generatedGravity);

        if (dash.magnitude > dashMagnitude)
        {
            finalVelocity += dash;
            StartCoroutine(Dash());
        }

        if (jump.magnitude > jumpMagnitude)
        {
            finalVelocity += jump;
            isInAir = true;
        }

        myCC.Move(finalVelocity * Time.deltaTime);

        jump = Vector3.Lerp(jump, Vector3.zero, playerSettings.jumpSmoothing * Time.deltaTime);
        dash = Vector3.Lerp(dash, Vector3.zero, playerSettings.dashSmoothing * Time.deltaTime);
    }

    private void GetInput()
    {
        if (!myCC.isGrounded) // Air Input
        {
            airbourneInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            airbourneInput.Normalize();
            airbourneInput = transform.TransformDirection(airbourneInput);

            groundedInput = Vector3.Lerp(groundedInput, Vector3.zero, playerSettings.momentumDampingSpeed * Time.deltaTime);
        }
        else // Grounded Input
        {
            airbourneInput = Vector3.zero;

            if (isCrouching && isRunning) // Sliding Input
            {
                isSliding = true;

                // slowly set last final Velocity sent from groundInput to 0
                groundedInput.x = Mathf.Lerp(groundedInput.x, 0, playerSettings.slideSlowSpeed * Time.deltaTime);
                groundedInput.z = Mathf.Lerp(groundedInput.z, 0, playerSettings.slideSlowSpeed * Time.deltaTime);

                // regular movement regained when groundInput's distance from 0 is small enough
                if (Mathf.Abs(groundedInput.x) < .175f && Mathf.Abs(groundedInput.z) < .175f)
                    isRunning = false;
            }
            else // Normal Input
            {
                isSliding = false;

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    groundedInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                    groundedInput.Normalize();
                    groundedInput = transform.TransformDirection(groundedInput);
                }
                else
                    groundedInput = Vector3.Lerp(groundedInput, Vector3.zero, playerSettings.groundMomentumDampingSpeed * Time.deltaTime);
            }
        }
    }

    private void ApplyGravity()
    {
        if (myCC.isGrounded && generatedGravity < playerSettings.realGravity)
            generatedGravity = playerSettings.realGravity;

        generatedGravity += playerSettings.realGravity * Time.deltaTime;
    }

    private void JumpAndClimbCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isCrouching && jumpCount > 0) //&& myCC.isGrounded
        {
            ResetVerticalVelocity();

            jumpModifier = 1f;

            if (isRunning)
                jumpModifier = 1.5f;

            // add jump here
            jump += Vector3.up.normalized * (playerSettings.jumpForce * jumpModifier) / playerSettings.mass;

            jumpCount--;
        }

        if (!isClimbing)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, wallRaycastDist) && !myCC.isGrounded)
            {
                if (hit.collider.GetComponent<Climbable>() != null)
                {
                    StartCoroutine(Climb(hit.collider));
                    //return;
                }
            }
        }

        if (isInAir) // Landing Sound Effect
            if (myCC.isGrounded && !isCrouching)
                isInAir = false;
    }

    private void DashCheck()
    {
        if (Input.GetMouseButtonDown(2) && !isCrouching)
        {
            if (!myCC.isGrounded && Input.GetKey(KeyCode.W) && playerStamina.playerStamina >= playerSettings.airDashStaminaCost)
            {
                playerStamina.playerStamina -= playerSettings.airDashStaminaCost;
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += Camera.main.transform.forward * (playerSettings.dashForce * 1f);
            }
            else if (!myCC.isGrounded && Input.GetKey(KeyCode.S) && playerStamina.playerStamina >= playerSettings.airDashStaminaCost)
            {
                playerStamina.playerStamina -= playerSettings.airDashStaminaCost;
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += -Camera.main.transform.forward * (playerSettings.dashForce * 1f);
            }
            else if (!myCC.isGrounded && Input.GetKey(KeyCode.A) && playerStamina.playerStamina >= playerSettings.airDashStaminaCost)
            {
                playerStamina.playerStamina -= playerSettings.airDashStaminaCost;
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += -Camera.main.transform.right * (playerSettings.dashForce * 1.8f);
            }
            else if (!myCC.isGrounded && Input.GetKey(KeyCode.D) && playerStamina.playerStamina >= playerSettings.airDashStaminaCost)
            {
                playerStamina.playerStamina -= playerSettings.airDashStaminaCost;
                ResetVerticalVelocity();
                audioSrc.PlayOneShot(dashSound);
                dash += Camera.main.transform.right * (playerSettings.dashForce * 1.8f);
            }

            else if (myCC.isGrounded) // is Grounded
            {
                if (Input.GetKey(KeyCode.W) && playerStamina.playerStamina >= playerSettings.groundDashStaminaCost)
                {
                    playerStamina.playerStamina -= playerSettings.groundDashStaminaCost;
                    audioSrc.PlayOneShot(dashSound);
                    dash += transform.forward * playerSettings.dashForce;
                }
                if (Input.GetKey(KeyCode.S) && playerStamina.playerStamina >= playerSettings.groundDashStaminaCost)
                {
                    playerStamina.playerStamina -= playerSettings.groundDashStaminaCost;
                    audioSrc.PlayOneShot(dashSound);
                    dash += -transform.forward * playerSettings.dashForce;
                }
                if (Input.GetKey(KeyCode.A) && playerStamina.playerStamina >= playerSettings.groundDashStaminaCost)
                {
                    playerStamina.playerStamina -= playerSettings.groundDashStaminaCost;
                    audioSrc.PlayOneShot(dashSound);
                    dash += -transform.right * playerSettings.dashForce;
                }
                if (Input.GetKey(KeyCode.D) && playerStamina.playerStamina >= playerSettings.groundDashStaminaCost)
                {
                    playerStamina.playerStamina -= playerSettings.groundDashStaminaCost;
                    audioSrc.PlayOneShot(dashSound);
                    dash += transform.right * playerSettings.dashForce;
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

    private void CeilingCheck()  //Debug Later
    {
        if (isCrouching)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position + ceilingCheckRayCastOffset, 1f, transform.up, out hit, ceilingCheckRayCastDist))
                obstacleOverhead = true;
            else
                obstacleOverhead = false;
        }
        else
            obstacleOverhead = false;
    }

    private void GroundCheck()
    {
        if (myCC.isGrounded)
        {
            jumpCount = playerSettings.allowedJumps;
        }
    }

    public void ResetVerticalVelocity()
    {
        jump.y = 0f;
        generatedGravity = 0f;
    }

    public void GetPlayersMoveSpeed()
    {
        isAiming = Input.GetMouseButton(1);

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isRunning)
            isRunning = !isRunning;

        initialSpeed = playerSettings.walkSpeed;

        if (isRunning)
            if ((finalVelocity.x == 0f && finalVelocity.z == 0f) || (Input.GetKeyUp(KeyCode.W) && !isCrouching) || playerSprintStamina.playerSprintStamina <= 0)
                isRunning = !isRunning;

        if (isCrouching)
            initialSpeed = playerSettings.crouchSpeed;

        if (isRunning && !isAiming)
            initialSpeed = playerSettings.runSpeed;
    }

    void GetPlayersSpeed()
    {
        playersActualSpeed = Vector3.Distance(playersLastPosition, transform.position) / Time.deltaTime;
        playersLastPosition = transform.position;
    }

    private IEnumerator Climb(Collider climbableCollider)
    {
        isClimbing = true;

        while (Input.GetKey(KeyCode.W))
        { 
            RaycastHit hit;
            Debug.DrawRay(transform.position + wallRaycastOffset, transform.forward * wallRaycastDist, Color.blue, 0.5f);
            if (Physics.Raycast(transform.position + wallRaycastOffset, transform.forward, out hit, wallRaycastDist))
            {
                if (hit.collider == climbableCollider)
                {
                    myCC.Move(new Vector3(0f, playerSettings.climbSpeed * Time.deltaTime, 0f));
                    yield return null;
                }
                else
                    break;
            }
            else
                break;
        }

        jump.y = 0f;
        generatedGravity = playerSettings.realGravity;
        isClimbing = false;
    }
    
    private IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(.3f);
        isDashing = false;
    }
}
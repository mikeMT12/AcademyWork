using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody rb;
    public float walkSpeed;
    public float airMinSpeed;  
    public float speedIncreaseMultiplier;
    public float groundDrag;
    private float moveSpeed;
    private float desiredMoveSpeed;
    private Vector3 nowspeed;
    private float lastDesiredMoveSpeed;
    private Vector3 moveDirection;
    private bool keepMomentum;

    [Header("Jumping")]
    private bool jumped;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;

    [Header("Keybinds")]
    float horizontalInput;
    float verticalInput;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool groundedSPH;

    [Header("Player")]
    [SerializeField] private Transform orientation;
    public static PlayerInput playerInput;

    [Header("Animation Controller")]
    [SerializeField] private Animator animator;
    public string isRunning = "isRunning";
    public string isJumping = "isJumping";
    public string isFlying = "isFlying";

    [Header("Variabels Out")]
    public MovementState state;

    [Header("Level")]
    [SerializeField] private SpawnPoint spawnPoint;
    public Vector3 checkPoint;

    [Header("Sound")]
    [SerializeField] private SoundAndMusicSystem soundSystem;


    public enum MovementState
    {
        staing,
        walking,
        air 
    }


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.CharacterControls.Enable();
        playerInput.CharacterControls.Jump.performed += CheckJump;
        playerInput.CharacterControls.Jump.started += CheckJump;
        rb = GetComponent<Rigidbody>();
        spawnPoint.SpawnPlayer(this.gameObject);
        checkPoint = spawnPoint.transform.position;
    }

    private void Start()
    {
        state = MovementState.staing;
        rb.freezeRotation = true;
        jumped = false;
        readyToJump = true;

    }

    private void Update()
    {
        /// ground check
        grounded = Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        groundedSPH = Physics.CheckSphere(transform.position, 0.2f);

        /// handlers
        MyInput();
        SpeedControl();
        StateHandler();
        AnimationHandler();

        /// handle drag
        if (state == MovementState.walking || state == MovementState.staing)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        /// get player input 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void CheckJump(InputAction.CallbackContext context)
    {
        /// jump input
        if (readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
            
        }
        StartCoroutine(JumpCooldown(context));
    }


    private void StateHandler()
    {
        /// Mode - Walking
        if (grounded)
        {
            jumped = false;
            nowspeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (nowspeed.magnitude > 2)
            {
                state = MovementState.walking;
            }

            else
            {
                state = MovementState.staing;
            }
              
            desiredMoveSpeed = walkSpeed;
        }

        /// Mode - Air
        else
        {
            state = MovementState.air;

            if (moveSpeed < airMinSpeed)
                desiredMoveSpeed = airMinSpeed;

            if (jumped)
            {
                jumped = false;
            }
        }


        ///smooth stopping
        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;

        /// deactivate keepMomentum
        if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) keepMomentum = false;
    }

    private void AnimationHandler()
    {  
        /// animaton controller
        if(state == MovementState.staing)
        {
            animator.SetBool(isRunning, false);
            animator.SetBool(isFlying, false);
            animator.SetBool(isJumping, false);
        }

        else if(state == MovementState.walking)
        {
            animator.SetBool(isRunning, true);
            animator.SetBool(isFlying, false);
            animator.SetBool(isJumping, false);
        }

        if (jumped == true)
        {
            animator.SetBool(isJumping, true);
        }

        if (state == MovementState.air)
        {
            animator.SetBool(isFlying, true);
        }
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        /// smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
            time += Time.deltaTime * speedIncreaseMultiplier;
            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer()
    { 
        /// calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        /// on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        /// in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);  
    }

    private void SpeedControl()
    {
        /// limiting speed on ground or in air
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        /// limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        /// reset y velocity
        jumped = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        soundSystem.jump.Play();
    }

    private void ResetJump()
    {
        /// reset jump
        readyToJump = true;
    }

    private IEnumerator JumpCooldown(InputAction.CallbackContext context)
    {
        /// check if space is hold 
        while(context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            if (readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            yield return new WaitForSeconds(jumpCooldown);
        }
    }

    public void LoadCheckPoint()
    {
        /// check point load (if needed) 
        transform.position = checkPoint;
    }

    private void OnDestroy()
    {
        ///usubscribe from actions
        playerInput.CharacterControls.Jump.performed -= CheckJump;
        playerInput.CharacterControls.Jump.started -= CheckJump;
    }

}


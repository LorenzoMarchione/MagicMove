using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngineInternal;

public class Player : MonoBehaviour
{
    //different player states to use
    private PlayerState currentState;
    public PlayerIdleState idleState;
    public PlayerRunState runState;
    public PlayerJumpState jumpState;
    public PlayerFallState fallState;

    //movement input
    public Vector2 move;
    public bool sprint = false;
    public bool tryCrouching = false;
    public bool crouching = false;
    public bool jumpPressed = false;
    public bool jumpReleased = false;

    public float facing = 1;

    //check variables
    public bool IsGrounded { get; private set; }
    public bool IsUnderCeiling { get; private set; }

    [Header("Movement settings")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    [Header("Jump settings")]
    [SerializeField] private float jumpWindowDuration;

    [Header("Check settings")]
    [SerializeField] private float groundCheckLength;
    [SerializeField] private float ceilingCheckWidth;
    [SerializeField] private float ceilingCheckHeight;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Transform ceilingCheckPos;

    [Header("Physics settings")]
    public float jumpHalt;
    public float upGravity;
    public float downGravity;
    public float normalGravity;

    [Header("Slide settings")]
    [SerializeField] private float slideDuration;
    [SerializeField] private float slideLockDuration;
    [SerializeField] private float slideSpeed;
    private bool sliding = false;
    private bool slideLock = false;

    [Header("Crouch hitbox")]
    [SerializeField] private float crouchHitboxYMult = 0.5f;
    private float normalHitboxY;
    private float crouchHitboxY;
    private float normalOffset;
    private float crouchOffset;

    [Header("Layers")]
    [SerializeField] private LayerMask floor;

    //components
    public Rigidbody2D rb;
    public PlayerInput input;
    public Transform transform;
    public Animator anim;
    public CapsuleCollider2D box;
    
    void Start()
    {
        //component setters
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        box = GetComponent<CapsuleCollider2D>();

        //creating to be used states
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        jumpState = new PlayerJumpState(this);
        fallState = new PlayerFallState(this);

        ChangeState(idleState);

        //crouch hitbox setters
        normalHitboxY = box.size.y;
        normalOffset = box.offset.y;
        crouchHitboxY = box.size.y * crouchHitboxYMult;
        crouchOffset = box.offset.y - crouchHitboxY / 2;
    }
    void Update()
    {
        currentState.Update();
        GroundCheck();
        CeilingCheck();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
        CrouchSlide();
    }
    public void ChangeState(PlayerState state)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
    }


    //start sliding state 
    private void CrouchSlide()
    {
        crouching = tryCrouching || IsUnderCeiling;
        if (crouching && jumpPressed && !sliding && !slideLock)
        {
            StartCoroutine(Slide());
            jumpPressed = false;
        }
        else if (sliding)
        {
            rb.linearVelocityX = facing * slideSpeed;
            box.size = new Vector2(box.size.x, crouchHitboxY);
            box.offset = new Vector2(box.offset.x, crouchOffset);
        }
        else if (crouching)
        {
            box.size = new Vector2(box.size.x, crouchHitboxY);
            box.offset = new Vector2(box.offset.x, crouchOffset);
        }
        else
        {
            box.size = new Vector2(box.size.x, normalHitboxY);
            box.offset = new Vector2(box.offset.x, normalOffset);
        }
    }
    //all animation checks
    private void AnimStates()
    {
        //anim.SetBool("isRunning", IsGrounded && Mathf.Abs(rb.linearVelocityX) > 0.1 && !crouching);
        //anim.SetBool("isJumping", !IsGrounded && rb.linearVelocityY > 0.1);
        //anim.SetBool("isFalling", !IsGrounded && rb.linearVelocityY < -0.1);
        //anim.SetBool("isIdle", IsGrounded && Mathf.Abs(rb.linearVelocityX) < 0.1 && !crouching);
        anim.SetBool("isCrouching", IsGrounded && crouching && !sliding);
        anim.SetBool("isSliding", IsGrounded && sliding);
    }
    //move input and crouch check
    private void OnMove(InputValue input)
    {
        move = input.Get<Vector2>(); 
        tryCrouching = move.y < -0.1;
    }
    //jump input logic
    private void OnJump(InputValue input)
    {
        if (input.isPressed)
        {
            jumpPressed = input.isPressed;
            StartCoroutine(JumpWindow());
        }
        else
            jumpReleased = true;
    }
    //sprint button
    private void OnSprint(InputValue input)
    {
        if (input.isPressed)
            sprint = true;
        else
            sprint = false;
    }
    //coroutine to allow jumping with slight earlier input
    private IEnumerator JumpWindow()
    {
        yield return new WaitForSeconds(jumpWindowDuration);
        if(jumpPressed)
            jumpPressed = false;
    }
    //coroutine controlling slide timers
    private IEnumerator Slide()
    {
        sliding = true;
        yield return new WaitForSeconds(slideDuration);
        sliding = false;
        slideLock = true;
        yield return new WaitForSeconds(slideLockDuration);
        slideLock = false;
    }
    //make player face opposite direction
    public void FLip()
    {
        facing = -transform.localScale.x;
        transform.localScale = new Vector3(facing, 1, 1);
    }
    //check if player is touching the ground
    private void GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckLength, groundCheckLength), 0, floor);
        IsGrounded = hit;
    }
    private void CeilingCheck()
    {
        Collider2D hit = Physics2D.OverlapBox(ceilingCheckPos.position, new Vector2(ceilingCheckWidth, ceilingCheckHeight), 0, floor);
        IsUnderCeiling = hit;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckLength, groundCheckLength));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ceilingCheckPos.position, new Vector3(ceilingCheckWidth, ceilingCheckHeight));
    }
}

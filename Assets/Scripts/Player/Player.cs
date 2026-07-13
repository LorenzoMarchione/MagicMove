using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngineInternal;

public class Player : MonoBehaviour
{ 
    //lateral movement input
    private Vector2 move;
    private bool sprint = false;
    private bool crouching = false;
    private bool jumpPressed = false;
    private bool jumpReleased = false;

    private float facing = 1;

    //check variables
    private bool isGrounded;

    [Header("Movement settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Jump settings")]
    [SerializeField] private float jumpWindowDuration;

    [Header("Check settings")]
    [SerializeField] private float groundCheckLength;
    [SerializeField] private Transform groundCheckPos;

    [Header("Physics settings")]
    [SerializeField] private float jumpHalt;
    [SerializeField] private float upGravity;
    [SerializeField] private float downGravity;
    [SerializeField] private float normalGravity;

    [Header("Slide settings")]
    [SerializeField] private float slideDuration;
    [SerializeField] private float slideLockDuration;
    [SerializeField] private float slideSpeed;
    private bool sliding = false;
    private bool slideLock = false;

    [Header("Layers")]
    [SerializeField] private LayerMask floor;

    //components
    private Rigidbody2D rb;
    private PlayerInput input;
    private Transform transform;
    private Animator anim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        GroundCheck();
        AnimStates();
    }
    private void FixedUpdate()
    {
        CrouchSlide();
        if (!sliding)
        {
            Movement();
            Jump();
        }
        ApplyVariableGravity();
    }
    //move player based on move input and sprint input, also flip to face move direction
    private void Movement()
    {
        float targetSpeed = sprint? runSpeed : walkSpeed;
        rb.linearVelocityX = targetSpeed * move.x;
        if (move.x < -0.1 && facing > 0)
            FLip();
        else if (move.x > 0.1 && facing < 0)
            FLip();
    }
    //jump physics
    private void Jump()
    {
        if (jumpPressed && isGrounded && !crouching)
        {
            jumpPressed = false;
            rb.linearVelocityY = 0;
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
        if (jumpReleased && rb.linearVelocityY > 0.1)
            rb.linearVelocityY *= jumpHalt;
        
        jumpReleased = false;
    }
    //start sliding state 
    private void CrouchSlide()
    {
        if(crouching && jumpPressed && !sliding && !slideLock)
        {
            StartCoroutine(Slide());
            jumpPressed = false;
        }
        else if(sliding)
            rb.linearVelocityX = facing * slideSpeed;
    }
    //all animation checks
    private void AnimStates()
    {
        anim.SetBool("isRunning", isGrounded && Mathf.Abs(rb.linearVelocityX) > 0.1 && !crouching);
        anim.SetBool("isJumping", !isGrounded && rb.linearVelocityY > 0.1);
        anim.SetBool("isFalling", !isGrounded && rb.linearVelocityY < -0.1);
        anim.SetBool("isIdle", isGrounded && Mathf.Abs(rb.linearVelocityX) < 0.1 && !crouching);
        anim.SetBool("isCrouching", isGrounded && crouching && !sliding);
        anim.SetBool("isSliding", isGrounded && sliding);
    }
    //move input and crouch check
    private void OnMove(InputValue input)
    {
        move = input.Get<Vector2>(); 
        crouching = move.y < -0.1;
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
    private void FLip()
    {
        facing = -transform.localScale.x;
        transform.localScale = new Vector3(facing, 1, 1);
    }
    //change gravity while on air for better feeling
    private void ApplyVariableGravity()
    {
        if (rb.linearVelocityY > 0.1f)
            rb.gravityScale = upGravity;
        else if (rb.linearVelocityY < -0.1f)
            rb.gravityScale = downGravity;
        else
            rb.gravityScale = normalGravity;
    }
    //check if player is touching the ground
    private void GroundCheck()
    {
        Collider2D hit = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckLength, groundCheckLength), 0, floor);
        isGrounded = hit;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckLength, groundCheckLength));
    }
}

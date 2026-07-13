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
    private bool jumpPressed = false;
    private bool jumpReleased = false;

    private float facing = 1;

    //check variables
    public bool isGrounded;

    [Header("Movement settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    [Header("Check settings")]
    [SerializeField] private float groundCheckLength;
    [SerializeField] private Transform groundCheckPos;

    [Header("Physics settings")]
    [SerializeField] private float jumpHalt;
    [SerializeField] private float upGravity;
    [SerializeField] private float downGravity;
    [SerializeField] private float normalGravity;

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
        Movement();
        Jump();
        ApplyVariableGravity();
    }
    //move player based on input and flip to face move direction
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
        if (jumpPressed && isGrounded)
        {
            jumpPressed = false;
            rb.linearVelocityY = 0;
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
        if (jumpReleased && rb.linearVelocityY > 0.1)
            rb.linearVelocityY *= jumpHalt;
        
        jumpReleased = false;
    }
    private void AnimStates()
    {
        if (isGrounded && Mathf.Abs(rb.linearVelocityX) > 0.1)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (!isGrounded && rb.linearVelocityY > 0.1)
            anim.SetBool("isJumping", true);
        else
            anim.SetBool("isJumping", false);

        if (!isGrounded && rb.linearVelocityY < -0.1)
            anim.SetBool("isFalling", true);
        else
            anim.SetBool("isFalling", false);

        if (isGrounded && Mathf.Abs(rb.linearVelocityX) < 0.1)
            anim.SetBool("isIdle", true);
        else
            anim.SetBool("isIdle", false);
    }
    //save only x axis from move controls
    private void OnMove(InputValue input)
    {
        move = input.Get<Vector2>(); 
    }
    //jump input logic
    private void OnJump(InputValue input)
    {
        if (input.isPressed)
            jumpPressed = input.isPressed;
        else
            jumpReleased = true;
    }
    private void OnSprint(InputValue input)
    {
        if (input.isPressed)
            sprint = true;
        else
            sprint = false;
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

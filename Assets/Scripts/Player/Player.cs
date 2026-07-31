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
    public PlayerCrouchState crouchState;
    public PlayerSlideState slideState;
    public PlayerAttackState attackState;
    public PlayerSpellCastState spellCastState;

    //movement input
    public Vector2 move;
    public bool sprint = false;
    public bool tryCrouching = false;
    public bool crouching = false;
    public bool jumpPressed = false;
    public bool jumpReleased = false;
    public bool attackPressed = false;
    public bool castPressed = false;

    public float facing = 1;

    //check variables
    public bool IsGrounded { get; private set; }
    public bool IsUnderCeiling { get; private set; }

    [Header("Movement settings")]
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float jumpForce;


    [Header("Floor Check settings")]
    [SerializeField] private float groundCheckLength;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask floor;

    [Header("Ceiling Check settings")]
    [SerializeField] private float ceilingCheckWidth;
    [SerializeField] private float ceilingCheckHeight;
    [SerializeField] private Transform ceilingCheckPos;
    //possible new layer for ceiling in the future

    [Header("Jump and fall settings")]
    [SerializeField] private float jumpWindowDuration;
    public float jumpHalt;
    public float upGravity;
    public float downGravity;
    public float normalGravity;

    [Header("Slide settings")]
    public float slideDuration;
    public float slideLockDuration;
    public float slideSpeed;
    public bool slideLock = false;

    [Header("Crouch hitbox")]
    [SerializeField] private float crouchHitboxYMult = 0.5f;
    public float normalHitboxY;
    public float crouchHitboxY;
    public float normalOffset;
    public float crouchOffset;

    //core components
    public Combat combat;

    //unity components
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

        combat = GetComponent<Combat>();

        //creating to be used states
        idleState = new PlayerIdleState(this);
        runState = new PlayerRunState(this);
        jumpState = new PlayerJumpState(this);
        fallState = new PlayerFallState(this);
        crouchState = new PlayerCrouchState(this);
        slideState = new PlayerSlideState(this);
        attackState = new PlayerAttackState(this);
        spellCastState = new PlayerSpellCastState(this);

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
    }
    public void ChangeState(PlayerState state)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
    public void AnimationFinished()
    {
        currentState.OnAnimationFinished();
    }
    //move input and crouch check
    private void OnMove(InputValue input)
    {
        move = input.Get<Vector2>();
        if (move.x < 0)
            move.x = -1;
        if (move.x > 0)
            move.x = 1;
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
        sprint = input.isPressed;
    }
    private void OnAttack(InputValue input)
    {
        attackPressed = input.isPressed;
    }
    private void OnSpellCast(InputValue input)
    {
        castPressed = input.isPressed;
    }
    //coroutine to allow jumping with slight earlier input
    private IEnumerator JumpWindow()
    {
        yield return new WaitForSeconds(jumpWindowDuration);
        if(jumpPressed)
            jumpPressed = false;
    }
    private IEnumerator SlideLocked()
    {
        slideLock = true;
        yield return new WaitForSeconds(slideLockDuration);
        slideLock = false;
    }
    public void LockSlide()
    {
        StartCoroutine(SlideLocked());
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
    //check if player has something directly over his head
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

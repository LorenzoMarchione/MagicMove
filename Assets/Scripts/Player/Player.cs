using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngineInternal;

public class Player : MonoBehaviour
{
    private PlayerState currentState;
    //different player states to use
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerCrouchState CrouchState { get; private set; }
    public PlayerSlideState SlideState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerSpellCastState SpellCastState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }


    //input
    public Vector2 Move { get; private set; }
    public bool Sprint { get; private set; }
    public bool TryCrouching { get; private set; }
    public bool Crouching { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool CastPressed { get; private set; }
    public bool NextPressed { get; private set; }
    public bool PreviousPressed { get; private set; }
    public bool InteractPressed { get; private set; }

    public float Facing { get; private set; }

    //check variables
    public bool IsGrounded { get; private set; }
    public bool IsUnderCeiling { get; private set; }
    public bool IsWallInFront { get; private set; }

    [Header("Movement settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float wallJumpForceX;
    [SerializeField] private float wallJumpForceY;

    public float WalkSpeed { get => walkSpeed; }
    public float RunSpeed { get => runSpeed; }
    public float CrouchSpeed { get => crouchSpeed; }
    public float JumpForce { get => jumpForce; }
    public float WallJumpForceX { get => wallJumpForceX; }
    public float WallJumpForceY { get => wallJumpForceY; }

    [Header("Floor Check settings")]
    [SerializeField] private float groundCheckLength;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask floor;

    [Header("Ceiling Check settings")]
    [SerializeField] private float ceilingCheckWidth;
    [SerializeField] private float ceilingCheckHeight;
    [SerializeField] private Transform ceilingCheckPos;
    //possible new layer for ceiling in the future

    [Header("Wall at feet Check Settings")]
    [SerializeField] private float wallCheckLenght;
    [SerializeField] private Transform wallCheckpos;

    [Header("Jump and fall settings")]
    [SerializeField] private float jumpWindowDuration;
    [SerializeField] private float jumpHalt;
    [SerializeField] private float upGravity;
    [SerializeField] private float downGravity;
    [SerializeField] private float normalGravity;
    public float JumpHalt { get => jumpHalt; }
    public float UpGravity { get => upGravity; }
    public float DownGravity { get => downGravity; }
    public float NormalGravity { get => normalGravity; }

    [Header("Slide settings")]
    [SerializeField] private float slideDuration;
    [SerializeField] private float slideLockDuration;
    [SerializeField] private float slideSpeed;
    [SerializeField] private bool slideLock = false;
    public float SlideDuration { get => slideDuration; }
    public float SlideSpeed { get => slideSpeed; }
    public bool SlideLock { get => slideLock; }

    [Header("Crouch hitbox")]
    [SerializeField] private float crouchHitboxYMult = 0.5f;
    public float NormalHitboxY { get; private set; }
    public float CrouchHitboxY { get; private set; }
    public float NormalOffset { get; private set; }
    public float CrouchOffset { get; private set; }

    //core components
    public Combat Combat { get; private set; }
    public Magic Magic { get; private set; }

    //unity components
    public Rigidbody2D Rb { get; private set; }
    public PlayerInput Input { get; private set; }
    public Transform Tf { get; private set; }
    public Animator Anim { get; private set; }
    public CapsuleCollider2D Box { get; private set; }

    void Start()
    {
        //unity component setters
        Rb = GetComponent<Rigidbody2D>();
        Input = GetComponent<PlayerInput>();
        Tf = GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        Box = GetComponent<CapsuleCollider2D>();

        //core component setters
        Combat = GetComponent<Combat>();
        Magic = GetComponent<Magic>();

        //creating to be used states
        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        CrouchState = new PlayerCrouchState(this);
        SlideState = new PlayerSlideState(this);
        AttackState = new PlayerAttackState(this);
        SpellCastState = new PlayerSpellCastState(this);
        WallJumpState = new PlayerWallJumpState(this);

        ChangeState(IdleState);

        //Facing direction set
        Facing = transform.localScale.x;

        //crouch hitbox setters
        NormalHitboxY = Box.size.y;
        NormalOffset = Box.offset.y;
        CrouchHitboxY = Box.size.y * crouchHitboxYMult;
        CrouchOffset = Box.offset.y - CrouchHitboxY / 2;
    }
    void Update()
    {
        currentState.Update();
        GroundCheck();
        CeilingCheck();
        WallAtFeetCheck();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    public void ChangeState(PlayerState state)
    {
        if(state == null)
        {
            Debug.Log("State empty");
            return;
        }

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
        Move = input.Get<Vector2>();
        if (Move.x < 0)
            Move = new Vector2(-1, Move.y);
        if (Move.x > 0)
            Move = new Vector2(1, Move.y);
        TryCrouching = Move.y < -0.1;
    }
    //jump input logic
    private void OnJump(InputValue input)
    {
        if (input.isPressed)
        {
            JumpPressed = input.isPressed;
            StartCoroutine(JumpWindow());
        }
        else
            JumpReleased = true;
    }
    //sprint button
    private void OnSprint(InputValue input)
    {
        Sprint = input.isPressed;
    }
    private void OnAttack(InputValue input)
    {
        AttackPressed = input.isPressed;
    }
    private void OnSpellCast(InputValue input)
    {
        CastPressed = input.isPressed;
    }
    private void OnNext(InputValue input)
    {
        if (input.isPressed)
            Magic.NextSpell();
    }
    private void OnPrevious(InputValue input)
    {
        if(input.isPressed)
            Magic.PreviousSpell();
    }
    private void OnInteract(InputValue input)
    {
        InteractPressed = input.isPressed;
    }
    //coroutine to allow jumping with slight earlier input
    private IEnumerator JumpWindow()
    {
        yield return new WaitForSeconds(jumpWindowDuration);
        if(JumpPressed)
            JumpPressed = false;
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
    public void ConsumeJump()
    {
        JumpPressed = false;
        JumpReleased = false;
    }
    public void ConsumeAttack() => AttackPressed = false;
    public void ConsumeSpell() => CastPressed = false;
    //make player face opposite direction
    public void FLip()
    {
        Facing = -transform.localScale.x;
        transform.localScale = new Vector3(Facing, 1, 1);
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
    private void WallAtFeetCheck()
    {
        Vector2 lineEndPoint = new Vector2(wallCheckpos.position.x + wallCheckLenght, wallCheckpos.position.y);
        RaycastHit2D hit = Physics2D.Raycast(wallCheckpos.position, new Vector2(Facing, 0), wallCheckLenght, floor);
        IsWallInFront = hit;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.orange;
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckLength, groundCheckLength));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(ceilingCheckPos.position, new Vector3(ceilingCheckWidth, ceilingCheckHeight));
        Gizmos.color= Color.green;
        Gizmos.DrawLine(wallCheckpos.position, new Vector3(wallCheckpos.position.x + wallCheckLenght, wallCheckpos.position.y, 0));
    }
}

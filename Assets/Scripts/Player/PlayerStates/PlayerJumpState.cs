using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player) : base(player)
    {
        animName = "isJumping";
    }

    public override void Enter()
    {
        base.Enter();

        JumpPressed = false;
        JumpReleased = false;
        
        //jump physics
        rb.linearVelocityY = 0;
        rb.AddForceY(player.jumpForce, ForceMode2D.Impulse);
        rb.gravityScale = player.upGravity;   
    }
    public override void Update()
    {

        if (player.IsWallInFront && JumpPressed)
            player.ChangeState(player.wallJumpState);
        else if (rb.linearVelocityY < -0.1 && !player.IsGrounded)
            player.ChangeState(player.fallState);
        else if (player.IsGrounded && rb.linearVelocityY < 0.1)
            player.ChangeState(player.idleState);
    }
    public override void FixedUpdate()
    {
        if (JumpReleased && rb.linearVelocityY > 0.1)
            rb.linearVelocityY *= player.jumpHalt;

        //move player based on move input and sprint input, also flip to face move direction
        float targetSpeed = Sprint ? player.runSpeed : player.walkSpeed;
        rb.linearVelocityX = targetSpeed * Move.x;
        if (Move.x < -0.1 && player.facing > 0)
            player.FLip();
        else if (Move.x > 0.1 && player.facing < 0)
            player.FLip();
    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = player.downGravity;
    }
}

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

        player.ConsumeJump();
        
        //jump physics
        rb.linearVelocityY = 0;
        rb.AddForceY(player.JumpForce, ForceMode2D.Impulse);
        rb.gravityScale = player.UpGravity;   
    }
    public override void Update()
    {

        if (player.IsWallInFront && JumpPressed)
            player.ChangeState(player.WallJumpState);
        else if (rb.linearVelocityY < -0.1 && !player.IsGrounded)
            player.ChangeState(player.FallState);
        else if (player.IsGrounded && rb.linearVelocityY < 0.1)
            player.ChangeState(player.IdleState);
    }
    public override void FixedUpdate()
    {
        if (JumpReleased && rb.linearVelocityY > 0.1)
            rb.linearVelocityY *= player.JumpHalt;

        //move player based on move input and sprint input, also flip to face move direction
        float targetSpeed = Sprint ? player.RunSpeed : player.WalkSpeed;
        rb.linearVelocityX = targetSpeed * Move.x;
        if (Move.x < -0.1 && player.Facing > 0)
            player.FLip();
        else if (Move.x > 0.1 && player.Facing < 0)
            player.FLip();
    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = player.DownGravity;
    }
}

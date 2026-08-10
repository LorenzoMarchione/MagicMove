using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(Player player): base(player)
    {
        animName = "isFalling";
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = player.DownGravity;
    }
    public override void Update()
    {

        if (player.IsWallInFront && JumpPressed)
            player.ChangeState(player.WallJumpState);
        else if (player.IsGrounded)
            player.ChangeState(player.IdleState);
    }
    public override void FixedUpdate()
    {
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

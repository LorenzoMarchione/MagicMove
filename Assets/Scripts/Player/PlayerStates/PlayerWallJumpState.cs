using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState (Player player) : base(player)
    {
        animName = "isWallJumping";
    }
    public override void Enter()
    {
        base.Enter();

        player.ConsumeJump();

        rb.linearVelocity = Vector2.zero;
        Vector2 wallJumpForce = new Vector2 (player.WallJumpForceX * -player.Facing, player.WallJumpForceY);
        rb.AddForce(wallJumpForce, ForceMode2D.Impulse);

        rb.gravityScale = player.UpGravity;

        player.FLip();
    }
    public override void Update()
    {
        if (player.IsWallInFront && JumpPressed)
            player.ChangeState(player.WallJumpState);
        else if (player.IsWallInFront)
            player.ChangeState(player.WallSlideState);
        else if (!player.IsGrounded && rb.linearVelocityY < -0.1)
            player.ChangeState(player.FallState);
        else if (player.IsGrounded && Mathf.Abs(rb.linearVelocityY) < 0.1)
            player.ChangeState(player.IdleState);
    }
}

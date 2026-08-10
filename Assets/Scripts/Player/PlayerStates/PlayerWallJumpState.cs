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


        JumpPressed = false;
        JumpReleased = false;

        rb.linearVelocity = Vector2.zero;
        Vector2 wallJumpForce = new Vector2 (player.wallJumpForceX * -player.facing, player.wallJumpForceY);
        rb.AddForce(wallJumpForce, ForceMode2D.Impulse);

        rb.gravityScale = player.upGravity;

        player.FLip();
    }
    public override void Update()
    {
        if (player.IsWallInFront && JumpPressed)
            player.ChangeState(player.wallJumpState);
        else if (player.IsGrounded && rb.linearVelocityY < 0.1)
            player.ChangeState(player.idleState);
    }
}

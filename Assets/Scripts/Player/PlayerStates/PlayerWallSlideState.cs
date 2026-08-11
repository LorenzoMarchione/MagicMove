using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState (Player player) : base(player)
    {
        animName = "isWallSliding";
    }
    public override void Enter()
    {
        base.Enter();

        rb.linearVelocityY = 0;
        rb.gravityScale = player.WallSlideGravity;
    }
    public override void Update()
    {
        if (player.IsWallInFront && JumpPressed)
            player.ChangeState(player.WallJumpState);
        else if (Move.x == -player.Facing || !player.IsWallInFront)
            player.ChangeState(player.FallState);
        else if (player.IsGrounded && Mathf.Abs(rb.linearVelocityY) < 0.1)
            player.ChangeState(player.IdleState);
    }
    public override void Exit()
    {
        base.Exit();

        rb.gravityScale = player.NormalGravity;
    }
}

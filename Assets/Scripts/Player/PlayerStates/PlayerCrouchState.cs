using UnityEngine;

public class PlayerCrouchState : PlayerState
{
    public PlayerCrouchState(Player player) : base(player)
    {
        animName = "isCrouching";
    }
    public override void Enter()
    {
        base.Enter();
        box.size = new Vector2(box.size.x, player.CrouchHitboxY);
        box.offset = new Vector2(box.offset.x, player.CrouchOffset);
    }
    public override void Update()
    {
        if (!TryCrouch && !player.IsUnderCeiling)
            player.ChangeState(player.IdleState);
        if (JumpPressed && !player.SlideLock)
            player.ChangeState(player.SlideState);
    }
    public override void FixedUpdate()
    {
        //move player based on move input and sprint input, also flip to face move direction
        rb.linearVelocityX = player.CrouchSpeed * Move.x;
        if (Move.x < -0.1 && player.Facing > 0)
            player.FLip();
        else if (Move.x > 0.1 && player.Facing < 0)
            player.FLip();
    }
    public override void Exit()
    {
        base.Exit();
        box.size = new Vector2(box.size.x, player.NormalHitboxY);
        box.offset = new Vector2(box.offset.x, player.NormalOffset);
    }
}

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
        box.size = new Vector2(box.size.x, player.crouchHitboxY);
        box.offset = new Vector2(box.offset.x, player.crouchOffset);
    }
    public override void Update()
    {
        if (!TryCrouch && !player.IsUnderCeiling)
            player.ChangeState(player.IdleState);
        if (JumpPressed && !player.slideLock)
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
        box.size = new Vector2(box.size.x, player.normalHitboxY);
        box.offset = new Vector2(box.offset.x, player.normalOffset);
    }
}

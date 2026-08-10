using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player) : base(player)
    {
        animName = "isRunning";
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        //changestate logic
        if (AttackPressed && combat.CanAttack)
            player.ChangeState(player.AttackState);
        else if (CastPressed && magic.CanCast(magic.CurrentSpell))
            player.ChangeState(player.SpellCastState);
        else if (JumpPressed)
            player.ChangeState(player.JumpState);
        else if (TryCrouch || player.IsUnderCeiling)
            player.ChangeState(player.CrouchState);
        else if (!player.IsGrounded && rb.linearVelocityY < -9)
            player.ChangeState(player.FallState);
        else if (Mathf.Abs(Move.x) < 0.1)
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
}

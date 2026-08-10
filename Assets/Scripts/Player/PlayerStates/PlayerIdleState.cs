using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player) 
    {
        animName = "isIdle";
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocityX = 0f;
    }
    public override void Update()
    {
        //changestate logic
        if (AttackPressed && combat.CanAttack)
            player.ChangeState(player.AttackState);
        else if (CastPressed && magic.CanCast(magic.CurrentSpell))
            player.ChangeState(player.SpellCastState);
        else if (Mathf.Abs(Move.x) > 0.1)
            player.ChangeState(player.RunState);
        else if (JumpPressed)
            player.ChangeState(player.JumpState);
        else if (TryCrouch || player.IsUnderCeiling)
            player.ChangeState(player.CrouchState);
        else if (!player.IsGrounded && rb.linearVelocityY < -9)
            player.ChangeState(player.FallState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}

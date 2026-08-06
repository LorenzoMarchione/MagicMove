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
        if (AttackPressed && combat.canAttack)
            player.ChangeState(player.attackState);
        else if (CastPressed && magic.CanCast(magic.CurrentSpell))
            player.ChangeState(player.spellCastState);
        else if (Mathf.Abs(Move.x) > 0.1)
            player.ChangeState(player.runState);
        else if (JumpPressed)
            player.ChangeState(player.jumpState);
        else if (TryCrouch || player.IsUnderCeiling)
            player.ChangeState(player.crouchState);
        else if (!player.IsGrounded && rb.linearVelocityY < -9)
            player.ChangeState(player.fallState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}

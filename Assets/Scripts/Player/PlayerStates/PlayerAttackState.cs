using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player) : base(player)
    {
        animName = "isAttacking1";
    }
    public override void Enter()
    {
        base.Enter();
        rb.linearVelocityX = 0f;
        player.ConsumeAttack();
    }
    public override void OnAnimationFinished()
    {
        if (Mathf.Abs(Move.x) > 0.1)
            player.ChangeState(player.RunState);
        else
            player.ChangeState(player.IdleState);
    }
}

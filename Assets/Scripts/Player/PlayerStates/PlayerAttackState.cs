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
        AttackPressed = false;
    }
    public override void OnAnimationFinished()
    {
        if (Mathf.Abs(Move.x) > 0.1)
            player.ChangeState(player.runState);
        else
            player.ChangeState(player.idleState);
    }
}

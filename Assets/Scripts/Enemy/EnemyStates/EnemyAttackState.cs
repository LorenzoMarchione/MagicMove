using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy attacker) : base(attacker)
    {
        animName = "isAttacking1";
    }
    public override void Enter()
    {
        base.Enter();

        rb.linearVelocityX = 0f;
    }
    public override void OnAnimationFinished()
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}

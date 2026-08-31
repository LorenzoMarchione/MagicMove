using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected override string animName => "isAttacking1";
    public EnemyAttackState(Enemy attacker) : base(attacker) { }
    public override void Enter()
    {
        base.Enter();

        rb.linearVelocityX = 0f;
    }
    public override void OnAnimationFinished()
    {
        stateMachine.ChangeState(enemy.IdleState);
    }
}

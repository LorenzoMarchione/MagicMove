using UnityEngine;

public class EnemyRangedAttackState : EnemyState
{
    protected override string animName => "isRangeAttacking";
    
    public EnemyRangedAttackState (Enemy enemy) : base(enemy) { }
    public override void Enter()
    {
        base.Enter();

        enemy.StopMovementX();
    }
    public override void OnAnimationFinished()
    {
        if(senses.SeekPlayer()  == null)
            stateMachine.ChangeState(enemy.PatrolState);
        else
            stateMachine.ChangeState(enemy.ChaseState);
    }
}

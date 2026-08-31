using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected override string animName => "isIdle";
   public EnemyIdleState (Enemy enemy) : base(enemy) { }
    public override void Enter()
    {
        base.Enter();
        rb.linearVelocityX = 0;
    }
    public override void Update()
    {
        if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(enemy.PatrolState);
        else if (senses.IsOnMeleeRange())
            stateMachine.ChangeState(enemy.AttackState);
        else if (senses.FloorCheck() && senses.SeekPlayer() != null)
            stateMachine.ChangeState(enemy.ChaseState);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
   public EnemyIdleState (Enemy enemy) : base(enemy)
    {
        animName = "isIdle";
    }
    public override void Enter()
    {
        base.Enter();
        rb.linearVelocityX = 0;
    }
    public override void Update()
    {
        if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(stateMachine.PatrolState);
        else if (senses.IsOnMeleeRange())
            stateMachine.ChangeState(stateMachine.AttackState);
        else if (senses.FloorCheck() && senses.SeekPlayer() != null)
            stateMachine.ChangeState(stateMachine.ChaseState);
    }
}

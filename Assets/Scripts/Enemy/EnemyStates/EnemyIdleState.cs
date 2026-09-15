using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected override string animName => "isIdle";
   public EnemyIdleState (Enemy enemy) : base(enemy) { }
    public override void Enter()
    {
        base.Enter();
        enemy.MoveForward(0);
    }
    public override void Update()
    {
        if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(enemy.PatrolState);
        else if (senses.IsOnMeleeRange() && combat.CanAttack())
            stateMachine.ChangeState(enemy.AttackState);
        else if (senses.FloorCheck() && senses.SeekPlayer() != null && !senses.IsOnMeleeRange())
            stateMachine.ChangeState(enemy.ChaseState);
    }
    public override void FixedUpdate()
    {
        if(senses.SeekPlayer() != null)
            enemy.FaceTarget(senses.SeekPlayer());
    }
}

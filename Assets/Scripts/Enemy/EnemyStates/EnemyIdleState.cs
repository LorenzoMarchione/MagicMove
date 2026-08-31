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
        else if (senses.FloorCheck() && senses.SeekPlayer() != null)
            stateMachine.ChangeState(enemy.ChaseState);
    }
}

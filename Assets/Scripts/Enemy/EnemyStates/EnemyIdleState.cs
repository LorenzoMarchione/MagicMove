using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
   public EnemyIdleState (Enemy enemy, Player playerDetected) : base(enemy, playerDetected)
    {
        animName = "isIdle";
    }
    public override void Update()
    {
        if (player == null)
            stateMachine.ChangeState(stateMachine.PatrolState);
    }
}

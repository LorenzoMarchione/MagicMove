using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
   public EnemyIdleState (EnemyConfig config, Player playerDetected) : base(config, playerDetected)
    {
        animName = "isIdle";
    }
    public override void Update()
    {
        if (player == null)
            stateMachine.ChangeState(stateMachine.PatrolState);
    }
}

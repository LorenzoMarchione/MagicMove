using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy) : base(enemy)
    {
        animName = "isWalking";
    }
    public override void Update()
    {
        if (senses.SeekPlayer() != null)
            stateMachine.ChangeState(stateMachine.ChaseState);
    }
    public override void FixedUpdate()
    {
        if (!senses.FloorCheck() || senses.WallCheck())
            enemy.Flip();
        
        rb.linearVelocityX = config.PatrolSpeed * enemy.Facing;
    }
}

using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(Enemy enemy, Player playerDetected) : base(enemy, playerDetected)
    {
        animName = "isWalking";
    }
    public override void FixedUpdate()
    {
        if (!senses.FloorCheck() || senses.WallCheck())
            enemy.Flip();
        
        rb.linearVelocityX = config.PatrolSpeed * enemy.Facing;
    }
}

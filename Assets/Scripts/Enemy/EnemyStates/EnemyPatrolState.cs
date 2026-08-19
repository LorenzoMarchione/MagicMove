using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(EnemyConfig config, Player playerDetected) : base(config, playerDetected)
    {
        animName = "isWalking";
    }
    public override void FixedUpdate()
    {
        if (!senses.FloorCheck() || senses.WallCheck())
            config.Flip();
        rb.linearVelocityX = config.PatrolSpeed * config.Facing;
    }
}

using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    protected override string animName => "isWalking";
    public EnemyPatrolState(Enemy enemy) : base(enemy) { }
    public override void Update()
    {
        if (senses.SeekPlayer() != null)
            stateMachine.ChangeState(enemy.ChaseState);
    }
    public override void FixedUpdate()
    {
        if (!senses.FloorCheck() || senses.WallCheck())
            enemy.Flip();
        
        enemy.MoveForward(config.PatrolSpeed);
    }
}

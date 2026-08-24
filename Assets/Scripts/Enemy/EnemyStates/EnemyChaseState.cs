using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy) : base(enemy)
    {
        animName = "isWalking";
    }
    public override void Update()
    {
        if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(stateMachine.PatrolState);
        else if (!senses.FloorCheck()) 
            stateMachine.ChangeState(stateMachine.IdleState);
    }
    public override void FixedUpdate()
    {
        Transform playerTf = senses.SeekPlayer();
        int direction = enemy.transform.position.x < playerTf.position.x ? 1 : -1;
        if (enemy.Facing != direction)
            enemy.Flip();
        rb.linearVelocityX = enemy.Facing * config.ChaseSpeed;
    }
}

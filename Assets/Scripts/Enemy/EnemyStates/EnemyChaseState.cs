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
        else if (senses.IsOnMeleeRange())
            stateMachine.ChangeState(stateMachine.AttackState);
        else if (!senses.FloorCheck()) 
            stateMachine.ChangeState(stateMachine.IdleState);
    }
    public override void FixedUpdate()
    {
        Transform playerTf = senses.SeekPlayer();
        float distance = enemy.transform.position.x - playerTf.position.x;
        int direction = enemy.transform.position.x < playerTf.position.x ? 1 : -1;
        if (enemy.Facing != direction && Mathf.Abs(distance) < config.FlipThreshold)
            enemy.Flip();
        rb.linearVelocityX = enemy.Facing * config.ChaseSpeed;
    }
}

using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected override string animName => "isWalking";
    public EnemyChaseState(Enemy enemy) : base(enemy) { }
    public override void Update()
    {
        if (senses.IsOnMeleeRange())
            stateMachine.ChangeState(enemy.AttackState);
        else if (!senses.FloorCheck()) 
            stateMachine.ChangeState(enemy.IdleState);
        else if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(enemy.PatrolState);
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget(senses.SeekPlayer());
        //This prevents microstepping between melee attacks
        float chase = senses.IsOnMeleeRange() ? 0 : config.ChaseSpeed;
        enemy.MoveForward(chase);
    }
}

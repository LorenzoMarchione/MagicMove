using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected override string animName => "isWalking";
    public EnemyChaseState(Enemy enemy) : base(enemy) { }
    public override void Update()
    {
        if (senses.IsOnMeleeRange() && combat.CanAttack())
            stateMachine.ChangeState(enemy.AttackState);
        else if (senses.IsOnShootingRange() && combat.CanAttack())
            stateMachine.ChangeState(enemy.RangedAttackState);
        else if (senses.IsOnMeleeRange() || senses.IsOnShootingRange())
            stateMachine.ChangeState(enemy.IdleState);
        else if (!senses.FloorCheck())
            stateMachine.ChangeState(enemy.IdleState);
        else if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(enemy.PatrolState);
    }
    public override void FixedUpdate()
    {
        enemy.CurrentTarget = senses.SeekPlayer();
        enemy.FaceTarget(senses.SeekPlayer());
        enemy.MoveForward(config.ChaseSpeed);
    }
}

using UnityEngine;

public class EnemyChaseState : EnemyState
{
    protected override string animName => "isWalking";
    public EnemyChaseState(Enemy enemy) : base(enemy) { }
    public override void Update()
    {
        if (senses.SeekPlayer() == null)
            stateMachine.ChangeState(enemy.PatrolState);
        else if (senses.IsOnMeleeRange())
            stateMachine.ChangeState(enemy.AttackState);
        else if (!senses.FloorCheck()) 
            stateMachine.ChangeState(enemy.IdleState);
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget(senses.SeekPlayer());
    }
}

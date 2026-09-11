using UnityEngine;

public class EnemyDamagedState : EnemyState
{
    protected override string animName => "isDamaged";
    private int knockbackDir;
    private float knockbackTimer;
    public EnemyDamagedState(Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();

        enemy.PushTo(knockbackDir);
        knockbackTimer = config.KnockbackDuration;
    }
    public override void FixedUpdate()
    {
        knockbackTimer -= Time.fixedDeltaTime;
        if( knockbackTimer <= 0)
        {
            enemy.StopMovementX();

            stateMachine.ChangeState(enemy.IdleState);
        }
    }
    public void SetKnockbackDir(int knockbackDir) => this.knockbackDir = knockbackDir;
}

using UnityEngine;

public class PlayerDamagedState : PlayerState
{
    private int knockbackDir;
    private float knockbackTimer;
    public PlayerDamagedState (Player player) : base(player) 
    {
        animName = "isDamaged";
    }

    public override void Enter()
    {
        base.Enter();

        player.PushTo(knockbackDir);
        knockbackTimer = player.KnockbackDuration;
    }
    public override void FixedUpdate()
    {
        knockbackTimer -= Time.fixedDeltaTime;

        if (knockbackTimer <= 0) 
            player.ChangeState(player.IdleState);
    }
    public override void Exit()
    {
        base.Exit();

        player.StopMovementX();
    }
    public void SetKnockbackDir(int dir) => knockbackDir = dir;
}

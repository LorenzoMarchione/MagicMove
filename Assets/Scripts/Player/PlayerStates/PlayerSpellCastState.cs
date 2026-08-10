using UnityEngine;

public class PlayerSpellCastState : PlayerState
{
    public PlayerSpellCastState (Player player) : base(player)
    {
        animName = "isCasting";
    }
    public override void Enter()
    {
        base.Enter();

        rb.linearVelocityX = 0f;
        player.ConsumeSpell();
    }
    public override void OnAnimationFinished()
    {
        if (Mathf.Abs(Move.x) > 0)
            player.ChangeState(player.RunState);
        else
            player.ChangeState(player.IdleState);
    }
}

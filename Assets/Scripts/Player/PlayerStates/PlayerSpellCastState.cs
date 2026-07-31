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
        CastPressed = false;
    }
    public override void OnAnimationFinished()
    {
        if (Mathf.Abs(Move.x) > 0)
            player.ChangeState(player.runState);
        else
            player.ChangeState(player.idleState);
    }
}

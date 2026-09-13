using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player) : base(player)
    {
        animName = "isDead";
    }
    public override void Enter()
    {
        base.Enter();

        player.StopMovementX();
    }
}

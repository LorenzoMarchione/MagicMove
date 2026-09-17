using UnityEngine;

public class PlayerDeadState : PlayerState
{
    SceneChanger changer;
    public PlayerDeadState(Player player) : base(player)
    {
        animName = "isDead";
    }
    public override void Enter()
    {
        base.Enter();

        player.StopMovementX();
    }
    public override void OnAnimationFinished()
    {
        changer = new SceneChanger();
        changer.DeathScene();
    }
}

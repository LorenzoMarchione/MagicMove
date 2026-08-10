using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class PlayerSlideState : PlayerState
{
    float timer = 0f; 
    public PlayerSlideState (Player player) : base(player)
    {
        animName = "isSliding";
    }
    public override void Enter()
    {
        base.Enter();
        player.ConsumeJump();
        timer = player.SlideDuration;
        
        rb.linearVelocityX = player.SlideSpeed * player.Facing;
        box.size = new Vector2(box.size.x, player.CrouchHitboxY);
        box.offset = new Vector2(box.offset.x, player.CrouchOffset);
    }
    public override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            player.ChangeState(player.IdleState);
    }
    public override void Exit()
    {
        base.Exit();

        player.LockSlide();

        box.size = new Vector2(box.size.x, player.NormalHitboxY);
        box.offset = new Vector2(box.offset.x, player.NormalOffset);
    }
}

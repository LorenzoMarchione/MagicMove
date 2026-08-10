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
        timer = player.slideDuration;
        
        rb.linearVelocityX = player.slideSpeed * player.Facing;
        box.size = new Vector2(box.size.x, player.crouchHitboxY);
        box.offset = new Vector2(box.offset.x, player.crouchOffset);
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

        box.size = new Vector2(box.size.x, player.normalHitboxY);
        box.offset = new Vector2(box.offset.x, player.normalOffset);
    }
}

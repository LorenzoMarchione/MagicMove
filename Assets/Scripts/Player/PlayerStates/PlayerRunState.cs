using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player) : base(player)
    {
        animName = "isRunning";
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        //changestate logic
        if (AttackPressed && combat.canAttack)
            player.ChangeState(player.attackState);
        else if (JumpPressed)
            player.ChangeState(player.jumpState);
        else if (TryCrouch || player.IsUnderCeiling)
            player.ChangeState(player.crouchState);
        else if (!player.IsGrounded && rb.linearVelocityY < -9)
            player.ChangeState(player.fallState);
        else if (Mathf.Abs(Move.x) < 0.1)
            player.ChangeState(player.idleState);
    }
    public override void FixedUpdate()
    {
        //move player based on move input and sprint input, also flip to face move direction
        float targetSpeed = Sprint ? player.runSpeed : player.walkSpeed;
        rb.linearVelocityX = targetSpeed * Move.x;
        if (Move.x < -0.1 && player.facing > 0)
            player.FLip();
        else if (Move.x > 0.1 && player.facing < 0)
            player.FLip();
    }
}

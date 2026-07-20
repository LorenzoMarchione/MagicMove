using UnityEngine;

public abstract class PlayerState
{
    //animation variable name
    protected string animName;

    //player components
    protected Rigidbody2D rb;
    protected Transform tf;
    protected Animator anim;
    protected CapsuleCollider2D box;
    protected Player player;

    //player inputs
    protected Vector2 Move { get => player.move; }
    protected bool Sprint { get => player.sprint; }
    protected bool TryCrouch { get => player.tryCrouching; }
    protected bool JumpPressed { get => player.jumpPressed; set => player.jumpPressed = value;  }
    protected bool JumpReleased { get => player.jumpReleased; set => player.jumpReleased = value; }

    public PlayerState(Player player)
    {
        this.player = player;
        rb = player.rb;
        tf = player.transform;
        anim = player.anim;
        box = player.box;
    }
    public virtual void Enter() 
    { 
        anim.SetBool(animName, true);
    }
    public virtual void Exit() 
    {
        anim.SetBool(animName, false);
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }

}

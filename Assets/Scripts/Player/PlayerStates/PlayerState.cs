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

    protected Combat combat;
    protected Magic magic;

    //player inputs
    protected Vector2 Move { get => player.Move; }
    protected bool Sprint { get => player.Sprint; }
    protected bool TryCrouch { get => player.TryCrouching; }
    protected bool AttackPressed { get => player.AttackPressed; }
    protected bool CastPressed { get => player.CastPressed; }
    protected bool JumpPressed { get => player.JumpPressed; }
    protected bool JumpReleased { get => player.JumpReleased; }

    public PlayerState(Player player)
    {
        this.player = player;
        rb = player.rb;
        tf = player.transform;
        anim = player.anim;
        box = player.box;

        combat = player.combat;
        magic = player.magic;
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

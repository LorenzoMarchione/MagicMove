using UnityEditor;
using UnityEngine;

public abstract class EnemyState 
{
    protected Player player;
    protected EnemyConfig config;
    protected EnemyStateMachine stateMachine;
    protected EnemySenses senses;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected string animName;

    public EnemyState (EnemyConfig conf, Player playerDetected)
    {
        player = playerDetected;
        config = conf;
        stateMachine = conf.GetComponent<EnemyStateMachine>();
        senses = conf.GetComponent<EnemySenses>();
        rb = conf.GetComponent<Rigidbody2D>();
        anim = conf.GetComponent<Animator>();
    }

    public virtual void Enter()
    {
        anim.SetBool(animName, true);
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }
    public virtual void Exit()
    {
        anim.SetBool(animName, false);
    }
}

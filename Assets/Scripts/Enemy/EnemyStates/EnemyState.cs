using System;
using UnityEditor;
using UnityEngine;

public abstract class EnemyState 
{
    protected Player player;
    protected Enemy enemy;
    protected EnemyConfig config;
    protected EnemyStateMachine stateMachine;
    protected EnemySenses senses;
    protected EnemyCombat combat;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected virtual string animName => null;

    public EnemyState (Enemy enemy)
    {
        this.enemy = enemy;
        stateMachine = enemy.StateMachine;
        config = enemy.Config;
        senses = enemy.Senses;
        combat = enemy.Combat;
        rb = enemy.Rb;
        anim = enemy.Anim;
    }

    public virtual void Enter()
    {
        if(!String.IsNullOrEmpty(animName))
            anim.SetBool(animName, true);
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }
    public virtual void Exit()
    {
        if (!String.IsNullOrEmpty(animName))
            anim.SetBool(animName, false);
    }
}

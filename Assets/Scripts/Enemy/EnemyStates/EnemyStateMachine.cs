using UnityEngine;

public class EnemyStateMachine
{
    private EnemyState currentState;

    public void Initialize(EnemyState firstState)
    {
        ChangeState(firstState);
    }
    public void Update()
    {
        currentState.Update();
    }
    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    public void AnimationFinished()
    {
        currentState.OnAnimationFinished();
    }
    public void ChangeState(EnemyState newState)
    {
        if(currentState != null) 
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}

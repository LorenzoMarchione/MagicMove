using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState currentState;
    public EnemyIdleState IdleState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    private void Awake()
    {
        IdleState = new EnemyIdleState(GetComponent<Enemy>(), null);
        PatrolState = new EnemyPatrolState(GetComponent<Enemy>(), null);
    }
    private void Start()
    {
        ChangeState(IdleState);
    }
    private void Update()
    {
        currentState.Update();
    }
    private void FixedUpdate()
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

using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Components
    public EnemyConfig Config { get => config; }
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemySenses Senses { get; private set; }
    public EnemyCombat Combat { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField] private EnemyConfig config;
    
    public float Facing { get; private set; }

    //States
    public EnemyIdleState IdleState { get; private set; }
    public EnemyPatrolState PatrolState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }

    private void Awake()
    {
        Senses = GetComponent<EnemySenses>();
        Combat = GetComponent<EnemyCombat>();
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(GetComponent<Enemy>());
        PatrolState = new EnemyPatrolState(GetComponent<Enemy>());
        ChaseState = new EnemyChaseState(GetComponent<Enemy>());
        AttackState = new EnemyAttackState(GetComponent<Enemy>());

        Facing = transform.localScale.x;
    }
    private void Start()
    {
        StateMachine.Initialize(PatrolState);
    }
    private void Update()
    {
        StateMachine.Update();
    }
    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }
    public void Flip()
    {
        transform.localScale = new Vector3(-Facing, 1, 1);
        Facing = -Facing;
    }
    public void FaceTarget(Transform tf)
    {
        float deltaX = transform.position.x - tf.position.x;
        if(Mathf.Abs(deltaX) >= 1 && Facing * deltaX < 0)
        {
            Flip();
        }
    }
}

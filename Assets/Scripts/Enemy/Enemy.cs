using UnityEngine;

public class Enemy : MonoBehaviour
{
    //components
    public EnemyConfig Config { get => config; }
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemySenses Senses { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public float Facing { get; private set; }
    [SerializeField] private EnemyConfig config;

    private void Awake()
    {
        StateMachine = GetComponent<EnemyStateMachine>();
        Senses = GetComponent<EnemySenses>();
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        Facing = transform.localScale.x;
    }
    public void Flip()
    {
        transform.localScale = new Vector3(-Facing, 1, 1);
        Facing = -Facing;
    }
}

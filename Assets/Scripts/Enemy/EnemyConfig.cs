using UnityEngine;

public class EnemyConfig : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float seeDistance = 15f;
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float chaseSpeed = 10f;

    [Header("Checks Settings")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform floorCheckPoint;
    [SerializeField] private float floorCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform[] wallCheckPoints;
    [SerializeField] private float wallCheckDistance = 1f;
    public float SeeDistance { get => seeDistance; }
    public float PatrolSpeed { get => patrolSpeed; }
    public float ChaseSpeed { get => chaseSpeed; }
    public LayerMask PlayerLayer { get => playerLayer; }
    public LayerMask FloorLayer { get => floorLayer; }
    public Transform FloorCheckPoint { get => floorCheckPoint; }
    public float FloorCheckDistance { get => floorCheckDistance; }
    public LayerMask WallLayer { get => wallLayer; }
    public Transform[] WallCheckPoints { get => wallCheckPoints; }
    public float WallCheckDistance { get => wallCheckDistance; }
    public float Facing { get; private set; }
    private void Start()
    {
        Facing = transform.localScale.x;
    }
    public void Flip()
    {
        transform.localScale = new Vector3(-Facing, 1, 1);
        Facing = transform.localScale.x;
    }
}

using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float flipThreshold = 0.3f;
    [SerializeField] private float patrolSpeed = 5f;
    [SerializeField] private float chaseSpeed = 10f;

    [Header("Checks Settings")]
    [SerializeField] private float seeDistance = 15f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private float floorCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallCheckDistance = 1f;

    [Header("Combat Settings")]
    [SerializeField] private float meleeRange = 1f;
    [SerializeField] private float rangedRange;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private int rangedDamage = 10;

    public int MeleeDamage { get => meleeDamage; }
    public int RangedDamage { get => rangedDamage; }
    public float MeleeRange { get => meleeRange; }
    public float RangedRange { get => rangedRange; }
    public float SeeDistance { get => seeDistance; }
    public float FlipThreshold {  get => flipThreshold; }
    public float PatrolSpeed { get => patrolSpeed; }
    public float ChaseSpeed { get => chaseSpeed; }
    public LayerMask PlayerLayer { get => playerLayer; }
    public LayerMask FloorLayer { get => floorLayer; }
    public float FloorCheckDistance { get => floorCheckDistance; }
    public LayerMask WallLayer { get => wallLayer; }
    public float WallCheckDistance { get => wallCheckDistance; }
}

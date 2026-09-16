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

    [Header("General Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;

    [Header("Melee Combat Settings")]
    [SerializeField] private float meleeRange = 1f;
    [SerializeField] private int meleeDamage = 10;

    [Header("Ranged Combat Settings")]
    [SerializeField] private float rangedRange = 10f;
    [SerializeField] private int rangedDamage = 10;
    [SerializeField] private float projectileSpeed = 12f;
    [SerializeField] private float projectileLifeTime = 5f;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Damaged Settings")]
    [SerializeField] private float knockbackForce = 15f;
    [SerializeField] private float knockbackDuration = 0.25f;

    public int MeleeDamage => meleeDamage; 
    public int RangedDamage => rangedDamage; 
    public float MeleeRange => meleeRange; 
    public float ShootingRange => rangedRange; 
    public float AttackCooldown => attackCooldown;
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileLifeTime => projectileLifeTime;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float SeeDistance => seeDistance; 
    public float FlipThreshold => flipThreshold; 
    public float PatrolSpeed => patrolSpeed; 
    public float ChaseSpeed => chaseSpeed; 
    public LayerMask PlayerLayer => playerLayer; 
    public LayerMask FloorLayer => floorLayer; 
    public float FloorCheckDistance => floorCheckDistance; 
    public LayerMask WallLayer => wallLayer; 
    public float WallCheckDistance => wallCheckDistance; 
    public float KnockbackForce => knockbackForce;
    public float KnockbackDuration => knockbackDuration;
}

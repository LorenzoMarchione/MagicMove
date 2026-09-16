using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Enemy enemy;
    private EnemyConfig config;
    [SerializeField] private Transform meleePoint;
    [SerializeField] private Transform shootingPoint;
    private float lastAttackTime;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        config = enemy.Config;
        lastAttackTime = Time.time;
    }
    public void OnMeleeAttackAnimationTrigger()
    {
        MeleeAttack();
    }
    public void OnRangedAttacAnimationTrigger()
    {
        RangedAttack(enemy.CurrentTarget);
    }
    public bool CanAttack() => Time.time > lastAttackTime + config.AttackCooldown;
    private void MeleeAttack()
    {
        lastAttackTime = Time.time;
        Collider2D hit = Physics2D.OverlapCircle(meleePoint.position, config.MeleeRange, config.PlayerLayer);
        if (hit == null)
            return;
        if (hit.TryGetComponent<Health>(out Health hp))
            hp.ChangeHealth(-config.MeleeDamage, transform.position);
    }
    private void RangedAttack(Transform target)
    {
        lastAttackTime = Time.time;

        Vector2 direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        Projectile projectile = Instantiate(config.ProjectilePrefab, shootingPoint.position, rotation).GetComponent<Projectile>();
        projectile.Initialize(
            config.ProjectileSpeed, 
            config.RangedDamage, 
            direction,
            config.ProjectileLifeTime);
    }
}

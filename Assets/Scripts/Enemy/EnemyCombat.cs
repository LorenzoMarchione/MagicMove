using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Enemy enemy;
    private EnemyConfig config;
    [SerializeField] private Transform meleePoint;
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
}

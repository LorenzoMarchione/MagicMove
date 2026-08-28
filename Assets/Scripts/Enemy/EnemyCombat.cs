using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Enemy enemy;
    private EnemyConfig config;
    [SerializeField] private Transform meleePoint;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        config = enemy.Config;
    }
    public void OnMeleeAttackAnimationTrigger()
    {
        MeleeAttack();
    }
    private void MeleeAttack()
    {
        Health hp = Physics2D.OverlapCircle(meleePoint.position, config.MeleeRange, config.PlayerLayer).GetComponent<Health>();
        if (hp != null)
        {
            hp.ChangeHealth(config.MeleeDamage);
        }
    }
}

using UnityEngine;

public class Damage : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private Health health;

    private void OnEnable()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;
    }
    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
        health.OnDeath -= HandleDeath;
    }
    private void HandleDamage(Vector2 sourcePosition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        player.DamagedState.SetKnockbackDir(knockbackDir);
        player.ChangeState(player.DamagedState);
    }
    private void HandleDeath()
    {
        player.ChangeState(player.DeadState);
    }
}

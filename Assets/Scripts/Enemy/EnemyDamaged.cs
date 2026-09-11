using UnityEngine;

public class EnemyDamaged : MonoBehaviour
{
    private Enemy enemy;
    private Animator anim;
    private Health health;

    [Header("Death to bits FX")]
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private float rotationForce;
    [SerializeField] private float ejectionForce;
    [SerializeField] private float lifetime;

    private void OnEnable()
    {
        enemy = GetComponent<Enemy>();
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

        enemy.DamagedState.SetKnockbackDir(knockbackDir);
        enemy.StateMachine.ChangeState(enemy.DamagedState);
    }
    private void HandleDeath()
    {
        foreach (GameObject prefab in bodyParts)
        {
            GameObject part = Instantiate(prefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = part.GetComponent<Rigidbody2D>();

            Vector2 ejectDirection = new Vector2(Random.Range(-1f, 1), Random.Range(0f, 1f)).normalized;
            
            rb.AddForce(ejectDirection*ejectionForce, ForceMode2D.Impulse);
            rb.AddTorque(rotationForce, ForceMode2D.Impulse);
            Destroy(part, lifetime);
        }
        Destroy(gameObject);
    }
}

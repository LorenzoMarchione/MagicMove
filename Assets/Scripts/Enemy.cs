using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private float rotationForce;
    [SerializeField] private float ejectionForce;
    [SerializeField] private float lifetime;
    [SerializeField] private Animator anim;
    [SerializeField] private Health health;

    private void OnEnable()
    {
        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;

    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
        health.OnDeath -= HandleDeath;
    }
    private void HandleDamage()
    {
        anim.Play("Hit");
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

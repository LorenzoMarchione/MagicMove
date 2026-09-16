using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private int damage;
    private Vector2 direction;
    [SerializeField] LayerMask targetLayer;

    public void Initialize(float speed, int damage, Vector2 direction, float lifetime)
    {
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;

        Destroy(gameObject, lifetime);
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }
    private void FixedUpdate() => GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) == 0)
            return;
        Health health = collision.GetComponent<Health>();
        if(health != null)
        {
            health.ChangeHealth(-damage, transform.position);
            Destroy(gameObject);
        }
    }
}

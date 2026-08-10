using UnityEngine;

public class Combat : MonoBehaviour
{
    private Player player;

    [Header("Attack settings")]
    [SerializeField] private int atkDamage;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform hitPos;
    [SerializeField] private float hitRadius;
    [SerializeField] private float atkCooldown;
    [SerializeField] private Animator animFX;
    
    private float atkTimer;
    public bool CanAttack { get; private set; }

    private void Start()
    {
        player = GetComponent<Player>();
        CanAttack = true;
    }
    private void Update()
    {
        if (atkTimer > 0)
        {
            atkTimer -= Time.deltaTime;

            if(atkTimer <= 0)
                CanAttack = true;
        }
    }
    public void AttackAnimationFinished()
    {
        player.AnimationFinished();
        atkTimer = atkCooldown;
    }
    public void AttackTrigger()
    {
        if (!CanAttack)
            return;

        CanAttack = false;
        
        Collider2D hit = Physics2D.OverlapCircle(hitPos.position, hitRadius, enemyLayer);
        if (hit != null)
        {
            hit.gameObject.GetComponent<Health>().ChangeHealth(-atkDamage);
            animFX.Play("HitFX");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPos.position, hitRadius);
    }
}

using UnityEngine;

public class Combat : MonoBehaviour
{
    private Player player;

    [Header("Attack settings")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform hitPos;
    [SerializeField] private float hitRadius;
    [SerializeField] private float atkCooldown;
    [SerializeField] private Animator animFX;
    private float timer;
    public bool canAttack = true;
    public int atkDamage;


    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
                canAttack = true;
        }
    }
    public void AttackAnimationFinished()
    {
        player.AnimationFinished();
        timer = atkCooldown;
    }
    public void AttackTrigger()
    {
        if (!canAttack)
            return;

        canAttack = false;
        
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

using System;
using Unity.Mathematics;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Player player;

    [SerializeField] private float castCooldown;
    private float castTimer;
    public bool canCast = true;
    

    [Header("Teleport Settings")]
    public float range;
    public float playerRadius;
    public LayerMask obstacleLayer;

    [Header("Spark Settings")]
    public float areaOfEffect;
    public int sparkDamage;
    public LayerMask enemyLayer;
    public GameObject sparkVFXPrefab;
    public GameObject sparkRayVFXPrefab;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        if(castTimer > 0)
        {
            castTimer -= Time.deltaTime;

            if (castTimer <= 0)
                canCast = true;
        }
    }
    public void OnSpellAnimationFinished()
    {
        castTimer = castCooldown;
        CastSpell();
        player.AnimationFinished();
    }
    private void CastSpell()
    {
        if (!canCast)
            return;
        Spark();
        canCast = false;
    }
    private void Teleport()
    {
        float tpDistance = range * player.facing;
        Vector2 targetPos = new Vector2(player.transform.position.x + tpDistance, player.transform.position.y);

        Collider2D hit = Physics2D.OverlapCircle(targetPos, playerRadius, obstacleLayer);
        if(hit != null)
        {
            float step = 0.1f;
            Debug.Log("colision");
            do
            {
                targetPos -= new Vector2(step * player.facing, 0);
                hit = Physics2D.OverlapCircle(targetPos, playerRadius, obstacleLayer);
            }
            while(hit != null);
        }
        player.transform.position = targetPos;
    }
    private void Spark()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.transform.position, areaOfEffect, enemyLayer);

        foreach(Collider2D hit in hits)
        {
            //direccion de punto a hacia punto b
            Vector2 dir = player.transform.position - hit.transform.position;
            //transformar direccion en angulo radial, luego a grados, luego a quaternion
            Quaternion angle = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            //distancia entra jugador y enemigo (dividido por escala aproximada del sprite y unidad global)
            float rayLenght = Vector2.Distance(player.transform.position, hit.transform.position) / 6.5f;
            //punto medio entra jugador y enemigo
            Vector2 rayPos = Vector2.Lerp(player.transform.position, hit.transform.position, 0.5f);
            
            GameObject rayVFX = Instantiate(sparkRayVFXPrefab, rayPos, angle);
            //escalar rayo segun distancia
            rayVFX.transform.localScale = new Vector3(rayLenght, 1, 1);

            GameObject spark = Instantiate(sparkVFXPrefab, hit.transform.position, Quaternion.identity);
            
            Health hp = hit.GetComponent<Health>();
            if(hp != null)
            {
                hp.ChangeHealth(-sparkDamage);
            }
            
            Destroy(spark, 4);
            Destroy(rayVFX, 4);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (player == null)
            return;
        Gizmos.DrawWireSphere(player.transform.position, playerRadius);
    }
}

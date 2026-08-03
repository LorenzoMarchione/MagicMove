using UnityEngine;
[CreateAssetMenu(menuName ="Spells/Spark Spell")]
public class SpellSparkSO : SpellSO
{
    public float areaOfEffect = 5f;
    public int sparkDamage = 5;
    public LayerMask enemyLayer;
    public GameObject sparkVFXPrefab;
    public GameObject sparkRayVFXPrefab;
    public override void Cast(Player player)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.transform.position, areaOfEffect, enemyLayer);

        foreach (Collider2D hit in hits)
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
            if (hp != null)
            {
                hp.ChangeHealth(-sparkDamage);
            }

            Destroy(spark, 4);
            Destroy(rayVFX, 4);
        }
    }
}

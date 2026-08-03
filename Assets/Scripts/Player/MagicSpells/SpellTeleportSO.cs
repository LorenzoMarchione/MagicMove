using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Teleport Spell")]
public class SpellTeleportSO : SpellSO
{
    public float range = 7.5f;
    public float playerRadius = 1f;
    public LayerMask obstacleLayer;
    public override void Cast(Player player)
    {
        float tpDistance = range * player.facing;
        Vector2 targetPos = new Vector2(player.transform.position.x + tpDistance, player.transform.position.y);

        Collider2D hit = Physics2D.OverlapCircle(targetPos, playerRadius, obstacleLayer);
        if (hit != null)
        {
            float step = 0.1f;
            Debug.Log("colision");
            do
            {
                targetPos -= new Vector2(step * player.facing, 0);
                hit = Physics2D.OverlapCircle(targetPos, playerRadius, obstacleLayer);
            }
            while (hit != null);
        }
        player.transform.position = targetPos;
    }
}

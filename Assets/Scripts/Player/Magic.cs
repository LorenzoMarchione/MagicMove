using JetBrains.Annotations;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Player player;
    public float range;
    public float playerRadius;
    public LayerMask obstacleLayer;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    public void OnSpellAnimationFinished()
    {
        player.AnimationFinished();
        CastSpell();
    }
    private void CastSpell()
    {
        Teleport();
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.transform.position, playerRadius);
    }
}

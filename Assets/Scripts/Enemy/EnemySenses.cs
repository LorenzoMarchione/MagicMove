using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [SerializeField] private EnemyConfig config;
    public Player SeekPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, config.SeeDistance, config.PlayerLayer);
        return collider.GetComponent<Player>();
    }
    public bool FloorCheck()
    {
         return Physics2D.Raycast(config.FloorCheckPoint.position, Vector2.down, config.FloorCheckDistance, config.FloorLayer);
    }
    public bool WallCheck()
    {
        foreach(Transform tf in config.WallCheckPoints)
        {
            if (Physics2D.Raycast(tf.position, new Vector2(config.Facing, 0), config.WallCheckDistance, config.WallLayer))
                return true;
        }
        return false;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, config.SeeDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(config.FloorCheckPoint.position, config.FloorCheckPoint.position + new Vector3(0, -config.FloorCheckDistance, 0));
        //No funciona, revisar 
        Gizmos.DrawLine(config.WallCheckPoints[0].position, config.WallCheckPoints[0].position + new Vector3(config.WallCheckDistance * config.Facing, 0, 0));
    }
}

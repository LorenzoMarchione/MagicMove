using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    private Enemy enemy;
    private EnemyConfig config;

    [SerializeField] private Transform floorCheckPoint;
    [SerializeField] private Transform[] wallCheckPoints;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        config = enemy.Config;
    }
    public Player SeekPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, config.SeeDistance, config.PlayerLayer);
        return collider.GetComponent<Player>();
    }
    public bool FloorCheck()
    {
         return Physics2D.Raycast(floorCheckPoint.position, Vector2.down, config.FloorCheckDistance, config.FloorLayer);
    }
    public bool WallCheck()
    {
        foreach(Transform tf in wallCheckPoints)
        {
            if (Physics2D.Raycast(tf.position, new Vector2(enemy.Facing, 0), config.WallCheckDistance, config.WallLayer))
                return true;
        }
        return false;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, config.SeeDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(floorCheckPoint.position, floorCheckPoint.position + new Vector3(0, -config.FloorCheckDistance, 0));
        Gizmos.DrawLine(wallCheckPoints[0].position, wallCheckPoints[0].position + new Vector3(config.WallCheckDistance * enemy.Facing, 0, 0));
        Gizmos.DrawLine(wallCheckPoints[1].position, wallCheckPoints[1].position + new Vector3(config.WallCheckDistance * enemy.Facing, 0, 0));
        Gizmos.DrawLine(wallCheckPoints[2].position, wallCheckPoints[2].position + new Vector3(config.WallCheckDistance * enemy.Facing, 0, 0));

    }
}

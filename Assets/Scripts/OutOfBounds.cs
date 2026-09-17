using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        player.GetComponent<Health>().ChangeHealth(-10000, Vector2.zero);
    }
}
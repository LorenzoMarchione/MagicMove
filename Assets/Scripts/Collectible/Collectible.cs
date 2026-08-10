using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool CanBeCollected => Time.time > collectTime;
    [SerializeField] private float collectDelay;
    private float collectTime;
    private Player player;

    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text textMessage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.GetComponent<Player>();
        if (player == null)
            return;
        Collect();
    }

    public void Initialize(ItemSO itemSO)
    {
        item = itemSO;
        spriteRenderer.sprite = itemSO.ItemSprite;

        collectTime = Time.time + collectDelay;
    }
    //si el jugador sale sin recoger item olvidar al player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            player = null;
    }
    private void Collect()
    {
        if(!CanBeCollected) return;
        textMessage.text = "Found " + item.name;
        anim.Play("CollectLoot");
        item.PickUp(player);
        Destroy(gameObject, 0.7f);
    }
}

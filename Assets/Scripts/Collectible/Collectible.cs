using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]private ItemSO item;
    private Player player;

    public Animator anim;
    public TMP_Text textMessage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.GetComponent<Player>();
        if (player == null)
            return;
        Collect();
    }
    //si el jugador sale sin recoger item olvidar al player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            player = null;
    }
    private void Collect()
    {
        textMessage.text = "Found " + item.name;
        anim.Play("CollectLoot");
        item.PickUp(player);
        Destroy(gameObject, 3);
    }
}

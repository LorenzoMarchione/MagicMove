using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<ItemSO> lootTable = new List<ItemSO>();
    [SerializeField] private float dropRate;
    [SerializeField] private float dropForce;
    [SerializeField] [Range(0, 1)] private float dropAngleRange;
    [SerializeField] private Animator anim;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision) => collision.TryGetComponent<Player>(out player);
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(player == null) return;
        if (player.InteractPressed)
            anim.Play("OpenChest");
    }
    private void OnTriggerExit2D(Collider2D collision) => player = null;
    private IEnumerator OpenChest()
    {
        foreach(ItemSO item in lootTable)
        {
            Vector2 dropDirection = new Vector2(Random.Range(-dropAngleRange, dropAngleRange), 1).normalized;
            Collectible drop = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Collectible>();
            drop.Initialize(item);
            Rigidbody2D rb = drop.GetComponent<Rigidbody2D>();
            rb.AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dropRate);
        }
    }
}

using UnityEngine;
[CreateAssetMenu(menuName = "Spells/Heal Spell")]
public class SpellHealSO : SpellSO
{
    public int healAmount = 10;
    public GameObject healVFXPrefab;
    public override void Cast(Player player)
    {
        GameObject heal = Instantiate(healVFXPrefab, player.transform.position, Quaternion.identity);
        Destroy(heal, 3);
        Health health = player.GetComponent<Health>();
        if (health != null )
        {
            health.ChangeHealth(healAmount);
        }
    }
}

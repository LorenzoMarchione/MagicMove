using UnityEngine;
public abstract class SpellSO : ItemSO
{
    [Header("General Settings")]
    public float cooldown;
    public bool offCooldown = true;

    public abstract void Cast(Player player);
    public override void PickUp(Player player) => player.magic.LearnSpell(this);
    
}

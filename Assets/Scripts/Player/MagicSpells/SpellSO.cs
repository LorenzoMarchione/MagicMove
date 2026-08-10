using UnityEngine;
public abstract class SpellSO : ItemSO
{
    [Header("General Settings")]
    [SerializeField] private float cooldown;
    public float Cooldown { get => cooldown; }

    public abstract void Cast(Player player);
    public override void PickUp(Player player) => player.Magic.LearnSpell(this);
    
}

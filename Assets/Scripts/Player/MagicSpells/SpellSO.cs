using UnityEngine;
public abstract class SpellSO : ScriptableObject
{
    [Header("General Settings")]
    public string name;
    public Sprite icon;
    public float cooldown;

    public abstract void Cast(Player player);
}

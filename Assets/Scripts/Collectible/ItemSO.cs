using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public abstract void PickUp(Player player);
}

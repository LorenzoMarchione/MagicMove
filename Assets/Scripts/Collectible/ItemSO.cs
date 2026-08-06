using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public Sprite itemSprite;

    public abstract void PickUp(Player player);
}

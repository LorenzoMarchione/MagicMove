using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite itemSprite;
    public string ItemName { get => itemName; }
    public Sprite Icon { get => icon; }
    public Sprite ItemSprite { get => itemSprite; }

    public abstract void PickUp(Player player);
}

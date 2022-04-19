using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemCategory
{
    NONE,
    WEAPON,
    CONSUMABLE,
    EQUIPMENT,
    AMMO
}
public abstract class ItemScript : ScriptableObject
{
    public string name = "Item";
    public ItemCategory itemCategory = ItemCategory.NONE;
    public GameObject itemPrefab;
    public bool isStackable;
    public int maxSize = 1;
    public Image icon;

    public delegate void AmountChange();
    public event AmountChange OnAmountChange;

    public delegate void ItemDestroyed();
    public event ItemDestroyed OnItemDestroyed;

    public delegate void ItemDropped();
    public event ItemDropped OnItemDropped;

    public int amountValue = 1; // current amount of the items in the inventory

    public PlayerController controller { get; private set; }

    public virtual void Initialize(PlayerController playerController)
    {
        controller = playerController;
    }

    public abstract void UseItem(PlayerController playerController);

    public virtual void DeleteItem(PlayerController playerController)
    {
        OnItemDestroyed?.Invoke();
        // delete item from inventory system here
        playerController.inventory.DeleteItem(this);
    }

    public virtual void DropItem(PlayerController playerController)
    {
        OnItemDropped?.Invoke();
    }

    public void ChangeAmount(int amount)
    {
        amountValue += amount;
        OnAmountChange?.Invoke();
    }

    public void SetAmount(int amount)
    {
        amountValue = amount;
        OnAmountChange?.Invoke();
    }
}

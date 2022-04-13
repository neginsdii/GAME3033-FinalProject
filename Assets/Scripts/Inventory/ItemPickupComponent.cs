using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupComponent : MonoBehaviour
{
    [SerializeField]
    private ItemScript pickupItem;

    [Tooltip("Manual override settings for drop amount, if left at -1, it will use the amount from the scriptable object")]
    [SerializeField]
    int amount = 1;

    [SerializeField] MeshRenderer propMeshRenderer;
    [SerializeField] MeshFilter propMeshFilter;

    ItemScript itemInstance;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateItem();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateItem()
    {
        itemInstance = Instantiate(pickupItem);
        if (amount > 0)
        {
            itemInstance.SetAmount(amount);
        }
        else
        {
            itemInstance.SetAmount(pickupItem.amountValue);
        }
        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (propMeshFilter) propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (propMeshRenderer) propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();
        if (playerInventory)
        {
            playerInventory.AddItem(itemInstance, amount);
        }

        if (itemInstance.itemCategory == ItemCategory.WEAPON)
        {
            WeaponHolder playerWeapon = other.GetComponent<WeaponHolder>();
            if (playerWeapon.GetEquippedWeapon != null)
            {
                playerWeapon.GetEquippedWeapon.weaponStats.totalBullets += pickupItem.amountValue;
            }
        }

        Destroy(gameObject);
    }
}

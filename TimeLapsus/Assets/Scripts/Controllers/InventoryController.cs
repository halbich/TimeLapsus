using System;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        UpdateInventory();
    }

    public GameObject InventoryItemTemplate;

    public void UpdateInventory()
    {
        var items = GameObject.FindGameObjectsWithTag("InventoryItem");
        foreach (var item in items)
        {
            Destroy(item);
        }
        for (var i = 0; i < Statics.Inventory.Count; ++i)
        {
            var itemInfo = Statics.Inventory[i];
            var newItem = Instantiate(InventoryItemTemplate);
            newItem.transform.SetParent(transform, false);
            
            var itemController = newItem.GetComponent<InventoryItemController>();
            if (itemController == null)
                throw new ArgumentException("InventoryItemTemplate does not contain an InventoryItemController");
            
            itemController.SetItem(itemInfo.ItemID);
            itemController.SetPosition(i);
        }
    }
}
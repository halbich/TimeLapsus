using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class InventoryController : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
        UpdateInventory();
    }
    public GameObject InventoryItemTemplate;
    private void Update()
    {
    }

    public void UpdateInventory()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("InventoryItem");
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        for (int i = 0; i < Statics.Inventory.Count; ++i)
        {
            var itemInfo = Statics.Inventory[i];
            var newItem = GameObject.Instantiate(InventoryItemTemplate);
            newItem.transform.SetParent(this.transform, false);
            InventoryItemController itemController = newItem.GetComponent<InventoryItemController>();
            if (itemController == null) throw new ArgumentException("InventoryItemTemplate does not contain an InventoryItemController");
            itemController.SetItem(itemInfo.ItemID);
            itemController.SetPosition(i);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class InventoryController : MonoBehaviour
    {
        public GameObject InventoryItemTemplate;
        private bool clickInitiated = false;
        private bool inventoryDisplayed;
        private void Update()
        {
            if (!Input.GetMouseButtonDown(1))
            {
                clickInitiated = true;
            }
            else if (clickInitiated)
            {
                clickInitiated = false;
                RightClick();
            }
        }
        private void RightClick()
        {
            if (inventoryDisplayed)
            {
                inventoryDisplayed = false;
                HideInventory();
            }
            else if (Statics.Inventory.Count != 0)
            {
                inventoryDisplayed = true;
                ShowInventory();
            }
        }

        private void ShowInventory()
        {
            for (int i = 0; i < Statics.Inventory.Count; ++i)
            {
                var itemInfo = Statics.Inventory[i];
                var newItem = GameObject.Instantiate(InventoryItemTemplate);
                newItem.tag = "InventoryItem";
                InventoryItemController itemController = newItem.GetComponent<InventoryItemController>();
                if (itemController == null) throw new ArgumentException("InventoryItemTemplate does not contain an InventoryItemController");
                itemController.SetItem(itemInfo.ItemID);
                itemController.SetPosition(i, Statics.Inventory.Count);
            }
        }

        private void HideInventory()
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag("InventoryItem");
            foreach (GameObject item in items)
            {
                Destroy(item);
            }

        }
    }
}

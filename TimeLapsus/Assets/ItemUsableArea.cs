using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class ItemUsableArea : ScriptWithController
{
    public List<ItemUseOnScript> UseActions;
    internal void Use(EnumItemID itemId)
    {
        foreach (var action in UseActions)
        {
            if (action.itemID == itemId)
            {
                action.Use();
                break;
            }
        }
    }
    protected void OnMouseEnter()
    {
        if (InventoryItemController.DraggedObject != null)
        {
            InventoryItemController.DraggedObject.DraggedOver = this;
            foreach (var action in UseActions)
            {
                if (action.itemID == InventoryItemController.DraggedObject.ItemId)
                {
                    Controller.DescriptionController.SetDescription(action.Name,true);
                    break;
                }
            }
        }
    }

    protected void OnMouseExit()
    {
        if (InventoryItemController.DraggedObject != null)
        {
            InventoryItemController.DraggedObject.DraggedOver = null;
            Controller.DescriptionController.SetDescription("", true);
        }
    }
}

using System.Collections.Generic;
using System.Linq;

public class ItemUsableArea : ScriptWithController
{
    public List<ItemUseOnScript> UseActions;

    internal void Use(EnumItemID itemId)
    {
        foreach (var action in UseActions.Where(action => action.itemID == itemId))
        {
            action.Use();
            break;
        }
    }

    protected void OnMouseEnter()
    {
        if (InventoryItemController.DraggedObject == null)
            return;

        InventoryItemController.DraggedObject.DraggedOver = this;

        foreach (var action in UseActions.Where(action => action.itemID == InventoryItemController.DraggedObject.ItemId))
        {
            Controller.DescriptionController.SetDescription(action.Name, true);
            break;
        }
    }

    protected void OnMouseExit()
    {
        if (InventoryItemController.DraggedObject == null)
            return;

        InventoryItemController.DraggedObject.DraggedOver = null;
        Controller.DescriptionController.SetDescription("", true);
    }
}
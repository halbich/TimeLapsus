using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class PickableItem : InspectObject
{

    public string pickedUpItemVariable;
    public EnumItemID itemId;

    protected Quest currentQuest = QuestController.Instance.GetCurrent();




    protected override void OnMouseDown()
    {
        if (!InspectController.IsInspected())
        {
            base.OnMouseDown();
            return;
        }

        if (!enabled || IsOverUI())
            return;

        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                PickUp();
                //QuestController.Instance.Inspect(EntityID);
            });
        else
            PickUp();
    }

    protected override void Start()
    {
        base.Start();

        bool pickedUpItem;
      
        if (currentQuest.TryGetValue(pickedUpItemVariable, out pickedUpItem) && pickedUpItem)
        {
            Destroy(gameObject);
            return;
        }

        if (InspectController.IsInspected())
        {
           IsInspected();
        }

    }

    public void PickUp()
    {
        Controller.AddInventoryItem(itemId);

        if (pickedUpItemVariable != null)
            currentQuest.SetValue(pickedUpItemVariable, true);

        Destroy(gameObject);
    }

    internal void IsInspected()
    {
        cursor = CursorType.PickUp;
    }
}
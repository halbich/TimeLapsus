﻿using System.Linq;
using UnityEngine;

public class PickableItem : ClickableArea
{
    private PickableItem()
    {
        cursor = CursorType.PickUp;
    }

    public string pickedUpItemVariable;
    public string examinedItemVariable;
    public EnumItemID itemId;

    protected Quest currentQuest = QuestController.Instance.GetCurrent();

    private DirectionPoint ObjectPoint;

    private void OnMouseDown()
    {
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
        bool examined;
        
        if (currentQuest.TryGetValue(pickedUpItemVariable, out pickedUpItem) && pickedUpItem)
        {
            Destroy(gameObject);
            return;
        }

        if (!(currentQuest.TryGetValue(examinedItemVariable, out examined)) || !examined)
        {
            enabled = false;
        }

        var comps = GetComponentInChildren<ItemPointScript>();
        if (comps == null)
            Debug.LogErrorFormat("No object pickable point defined for {0}! ", gameObject.name);
        else
        {
            ObjectPoint = comps.GetPoint(Controller.CharacterZPosition);
            Destroy(comps);
        }
    }

    public void PickUp()
    {
        Controller.AddInventoryItem(itemId);

        if (pickedUpItemVariable != null) 
            currentQuest.SetValue(pickedUpItemVariable, true);

        Destroy(gameObject);
    }
}
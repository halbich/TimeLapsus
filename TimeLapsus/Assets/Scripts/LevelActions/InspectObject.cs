using System;
using UnityEngine;

public class InspectObject : ClickableArea
{
    public InspectObject()
    {
        cursor = CursorType.Explore;
    }

    protected DirectionPoint ObjectPoint;

    protected InspectObjectController InspectController;

    protected virtual void OnMouseDown()
    {
        if (!enabled || IsOverUI())
            return;

        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                InspectController.Inspect();
            });
        else
            InspectController.Inspect();
    }

    protected override void Start()
    {
        base.Start();

        var comps = GetComponentInChildren<ItemPointScript>();
        if (comps == null)
            Debug.LogErrorFormat("No object item point defined for {0}! ", gameObject.name);
        else
        {
            ObjectPoint = comps.GetPoint(Controller.CharacterZPosition);
            // we can  have multiple object trying to init using this point
            //Destroy(comps);
        }

        InspectController = GetComponent<InspectObjectController>();
        if (InspectController == null)
            Debug.LogErrorFormat("No InspectObjectController defined for {0}! ", gameObject.name);
    }

    internal void SetCursor(CursorType cursorType)
    {
        cursor = cursorType;
    }
}
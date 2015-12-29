using UnityEngine;

public class WalkableArea : ClickableArea
{
    public WalkableArea()
    {
        cursor = CursorType.Walk;
    }

    private void OnMouseDown()
    {
        if (!IsInBox || IsOverUI())
            return;

        var v = Input.mousePosition;
        var point = Camera.main.ScreenToWorldPoint(v);

        Controller.PlayerController.MoveTo(point, () =>
        {

        });
    }

   
}
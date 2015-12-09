using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableArea : ScriptWithController
{
    public CursorType cursor = CursorType.Main;

    protected bool IsInBox = false;

   

    private void OnMouseEnter()
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        //    return;


        IsInBox = true;
        if (Controller && Controller.CursorManager)
            Controller.CursorManager.SetCursor(cursor);
    }

    private void OnMouseExit()
    {
        IsInBox = false;
        if (Controller && Controller.CursorManager)
            Controller.CursorManager.SetCursor();
    }
}
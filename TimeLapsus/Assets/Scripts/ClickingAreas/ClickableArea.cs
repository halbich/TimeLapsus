public class ClickableArea : ScriptWithController
{
    public CursorType cursor = CursorType.Main;

    protected bool IsInBox;

    protected bool IsUI;

    protected bool IsOverUI()
    {
        if (IsUI) return false;
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    protected void Update()
    {
        if (!(Controller && Controller.CursorManager)) return;
            if (IsInBox && !IsOverUI())
        {
            Controller.CursorManager.SetCursor(cursor);
        }
        else
        {
               //Controller.CursorManager.SetCursor();
        }
    }

    protected void OnMouseEnter()
    {

        IsInBox = true;
    }

    protected void OnMouseExit()
    {
        IsInBox = false;
        if (!(Controller && Controller.CursorManager)) return;
        Controller.CursorManager.SetCursor();
    }
}
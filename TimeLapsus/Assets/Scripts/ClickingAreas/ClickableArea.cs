public class ClickableArea : ScriptWithController
{
    protected CursorType cursor = CursorType.Main;

    protected bool IsInBox;

    public string Name;

    protected bool IsUI;

    protected bool IsOverUI()
    {
        return !IsUI && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    protected void Update()
    {
        if (!(Controller && Controller.CursorManager)) return;
        if (!IsInBox || IsOverUI() || !enabled)
            return;

        Controller.CursorManager.SetCursor(cursor);
        Controller.DescriptionController.SetDescription(Name, false);
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
        Controller.DescriptionController.SetDescription("", false);
    }
}
using UnityEngine;

public class ClickableArea : ScriptWithController
{
    protected CursorType cursor = CursorType.Main;

    protected bool IsInBox;

    [Tooltip("This name will ber visible as description")]
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
        if (!enabled)
            return;

        IsInBox = true;
    }

    protected void OnMouseExit()
    {
        if (!enabled)
            return;

        IsInBox = false;
        if (!(Controller && Controller.CursorManager)) return;
        Controller.CursorManager.SetCursor();
        Controller.DescriptionController.SetDescription("", false);
    }
}
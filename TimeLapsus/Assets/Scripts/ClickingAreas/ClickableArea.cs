using UnityEngine;

public class ClickableArea : ScriptWithController
{
    protected CursorType cursor = CursorType.Main;

    protected bool IsInBox = false;
    bool clickBegan;

    // Use this for initialization
    private void Start()
    {
    }

    protected virtual void OnMouseEnter()
    {
        IsInBox = true;
        Controller.CursorManager.SetCursor(cursor);
        Debug.Log(cursor);
    }

    protected virtual void OnMouseExit()
    {
        IsInBox = false;
        Controller.CursorManager.SetCursor();
    }
}
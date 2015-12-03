using UnityEngine;

public class ClickableArea : ScriptWithController
{
    protected CursorType cursor = CursorType.Main;

    protected bool IsInBox = false;

    // Use this for initialization
    private void Start()
    {
    }

    private void OnMouseEnter()
    {
        IsInBox = true;
        Controller.CursorManager.SetCursor(cursor);
        Debug.Log(cursor);
    }

    private void OnMouseExit()
    {
        IsInBox = false;
        Controller.CursorManager.SetCursor();
    }
}
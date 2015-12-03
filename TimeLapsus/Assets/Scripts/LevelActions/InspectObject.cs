using UnityEngine;
using System.Collections;

public class InspectObject : ScriptWithController
{

    private void OnMouseEnter()
    {
        Controller.CursorManager.SetCursor(CursorType.Explore);
    }

    private void OnMouseExit()
    {
        Controller.CursorManager.SetCursor();
    }
}

using UnityEngine;
using System.Collections;

public class SpeakingObject : ScriptWithController
{

	private void OnMouseEnter()
    {
        Controller.CursorManager.SetCursor(CursorType.Speak);
    }

    private void OnMouseExit()
    {
        Controller.CursorManager.SetCursor();
    }
}

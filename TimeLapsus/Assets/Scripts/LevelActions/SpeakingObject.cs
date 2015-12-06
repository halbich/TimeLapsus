using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakingObject : ScriptWithController
{
    public string Description;
    public List<string> Dialogue;
	private void OnMouseEnter()
    {
        Controller.CursorManager.SetCursor(CursorType.Speak);
    }

    private void OnMouseExit()
    {
        Controller.CursorManager.SetCursor();
    }
    private void OnMouseDown()
    {
        if (!Controller.DialogueActive)
        {
            Controller.DialogController.ShowMessages(Dialogue);
        }
    }
}

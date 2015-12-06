using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DialogController : ScriptWithController
{
    Queue<string> messagesToDisplay = new Queue<string>();
    public string DialogueText;
    bool clickInitiated = false;
    public void ShowMessages(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            messagesToDisplay.Enqueue(message);
        }
    }

    public void ShowMessage(string message)
    {
         messagesToDisplay.Enqueue(message);
    }
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            clickInitiated = true;
        }
        else if (clickInitiated)
        {
            clickInitiated = false;
            LeftClick();
        }
    }
    private void LeftClick()
    {
        if (messagesToDisplay.Any())
        {
            ShowFirstMessage();
        }
        else HideMessageBox();
    }
    void ShowFirstMessage()
    {
            Controller.DialogueActive = true;
        DialogueText = messagesToDisplay.Dequeue();
    }
    void HideMessageBox()
    {
        DialogueText = "";
        Controller.DialogueActive = false;
    }
}
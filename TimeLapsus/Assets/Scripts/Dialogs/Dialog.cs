using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{

    public Dialog()
    {
        DialogLines = new List<DialogLine>();
    }

    [SerializeField]
    public List<DialogLine> DialogLines;

    public static Dialog SimpleDialog(string lineID)
    {
        return new Dialog
        {
            DialogLines = new List<DialogLine>
           {
               new DialogLine(EnumActorID.MainCharacter, TextController.Instance.GetText(lineID))
            }
        };
    }
}

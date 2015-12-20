using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Dialog
{

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

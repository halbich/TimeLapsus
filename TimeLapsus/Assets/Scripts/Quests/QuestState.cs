using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Defines allowed Actions
public class QuestState
{
    public Dictionary<EnumActorID, Dialog> AllDialogs;


    public QuestState()
    {
        AllDialogs = new Dictionary<EnumActorID, Dialog>();
    }

    internal void Speak(EnumActorID actor, Sprite avatar)
    {
        DialogController.Instance.ShowDialog(AllDialogs[actor], avatar);
    }
}

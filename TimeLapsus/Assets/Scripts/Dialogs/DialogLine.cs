using System;
using UnityEngine;

[Serializable]
public class DialogLine
{
    public EnumActorID ActorID;

    public string Text;
    public AudioClip AudioFile = null;
    public DialogLine(EnumActorID actorID, string text, AudioClip audioFile)
    {
        AudioFile = audioFile;
        ActorID = actorID;
        Text = text;
    }
}
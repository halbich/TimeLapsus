using System;

[Serializable]
public class DialogLine
{
    public EnumActorID ActorID;

    public string Text;

    public DialogLine(EnumActorID actorID, string text)
    {
        ActorID = actorID;
        Text = text;
    }
}
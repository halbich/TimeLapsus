using System;

[Serializable]
public class DialogLine  {

    public EnumActorID ActorID;

    public string Text;

    public float Duration;

    public DialogLine(EnumActorID actorID, string text, float duration = 5f)
    {
        ActorID = actorID;
        Text = text;
        Duration = duration;
    }
}

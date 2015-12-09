using System.Collections.Generic;

public class Quest
{
    public QuestState CurrentState { get { return QuestStates.Peek(); } }

    public Quest()
    {
        QuestStates = new Queue<QuestState>();
    }

    private Queue<QuestState> QuestStates;

    internal void CreateTestQuest()
    {
        var firstState = new QuestState();
        firstState.AllDialogs.Add(EnumActorID.Mayor, new Dialog
        {
            DialogLines = new List<DialogLine>()
            {
                new DialogLine( EnumActorID.MainCharacter,"Zdravím, vypadáte jako muž, co by mi mohl pomoci ?!", 3.5f),
                new DialogLine( EnumActorID.Mayor,"#&@{~ˇ^ˇ^˘°^ˇ#&@#&{", 2.5f),
                new DialogLine( EnumActorID.MainCharacter,"Promiňte, asi jsem špatně rozuměl ?",3),
                new DialogLine( EnumActorID.Mayor,"&#{&@#{}}~ˇ^", 2.5f),
                new DialogLine( EnumActorID.MainCharacter,"Aha, spíš jsem nerozuměl vůbec. Kdybych tak měl nějaký univerzální překladač...", 5)
            }
        });
        firstState.AllDialogs.Add(EnumActorID.Potter, new Dialog
        {
            DialogLines = new List<DialogLine>()
            {
                new DialogLine( EnumActorID.MainCharacter,"Ahoj, dáš mi tamtu vázu?", 2.5f),
                new DialogLine( EnumActorID.Potter,"NOPE!",2),
                new DialogLine( EnumActorID.MainCharacter,"FFFFFFFUUuuuUUuUuuUuu!!!!"),
            }
        });
        QuestStates.Enqueue(firstState);
    }

    public bool QuestDone { get { return QuestStates.Count == 0; } }
}
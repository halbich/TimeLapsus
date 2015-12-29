using System.Collections.Generic;

public class Quest
{
    public bool QuestDone { get; private set; }

    private readonly Dictionary<string, object> StoredValues;

    public Quest()
    {
        StoredValues = new Dictionary<string, object>();
    }

    public bool TryGetValue<T>(string key, out T resultObject)
    {
        object result;
        if (StoredValues.TryGetValue(key, out result))
        {
            resultObject = (T)result;
            return true;
        }
        resultObject = default(T);
        return false;
    }

    public void SetValue(string key, object value)
    {
        if (StoredValues.ContainsKey(key))
            StoredValues[key] = value;
        else
            StoredValues.Add(key, value);
    }


    internal void CreateTestQuest()
    {
        //var firstState = new QuestState();
        //firstState.AllDialogs.Add(EnumActorID.Mayor, new Dialog
        //{
        //    DialogLines = new List<DialogLine>()
        //    {
        //        new DialogLine( EnumActorID.MainCharacter,"Zdravím, vypadáte jako muž, co by mi mohl pomoci ?!", 3.5f),
        //        new DialogLine( EnumActorID.Mayor,"#&@{~ˇ^ˇ^˘°^ˇ#&@#&{", 2.5f),
        //        new DialogLine( EnumActorID.MainCharacter,"Promiňte, asi jsem špatně rozuměl ?",3),
        //        new DialogLine( EnumActorID.Mayor,"&#{&@#{}}~ˇ^", 2.5f),
        //        new DialogLine( EnumActorID.MainCharacter,"Aha, spíš jsem nerozuměl vůbec. Kdybych tak měl nějaký univerzální překladač...", 5)
        //    }
        //});
        //firstState.AllDialogs.Add(EnumActorID.Potter, new Dialog
        //{
        //    DialogLines = new List<DialogLine>()
        //    {
        //        new DialogLine( EnumActorID.MainCharacter,"Ahoj, dáš mi tamtu vázu?", 2.5f),
        //        new DialogLine( EnumActorID.Potter,"NOPE!",2),
        //        new DialogLine( EnumActorID.MainCharacter,"FFFFFFFUUuuuUUuUuuUuu!!!!"),
        //    }
        //});
        //QuestStates.Enqueue(firstState);
    }


}
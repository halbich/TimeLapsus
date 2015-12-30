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

}
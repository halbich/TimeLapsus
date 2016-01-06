using System.Collections.Generic;
using UnityEngine;

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
        var k = key ?? string.Empty;
        k = k.Trim();

        object result;
        if (StoredValues.TryGetValue(k, out result))
        {
            resultObject = (T)result;
            return true;
        }
        resultObject = default(T);
        return false;
    }

    public void SetValue(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
            return;

        key = key.Trim();

        Debug.LogFormat("Set {1} to variable {0}", key, value);

        if (StoredValues.ContainsKey(key))
            StoredValues[key] = value;
        else
            StoredValues.Add(key, value);
    }

    public bool GetBoolean(string key)
    {
        bool res;
        return TryGetValue(key, out res) && res;
    }

    public void SetBoolean(string key, bool value = true)
    {
        SetValue(key, value);
    }
}
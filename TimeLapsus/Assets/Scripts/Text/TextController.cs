using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextController
{
    private readonly Dictionary<string, string> keys;
    private readonly bool isLoaded;

    private static TextController _inst;

    public static TextController Instance
    {
        get
        {
            // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
            if (_inst == null)
                _inst = new TextController();

            return _inst;
        }
    }

    private TextController()
    {
        keys = new Dictionary<string, string>();
        var r = Resources.Load("translation") as TextAsset;

        isLoaded = load(r.text);

        Debug.LogFormat("Načteno: {0}", keys.Count);
    }

    private bool load(string text)
    {
        // Handle any problems that might arise when reading the text
        try
        {
            using (var theReader = new StringReader(text))
            {
                string line;
                do
                {
                    line = theReader.ReadLine();

                    if (line == null)
                        continue;

                    var entries = line.Split('=');
                    if (entries.Length == 2)
                        addKeyValue(entries);
                    else
                    {
                        Debug.LogErrorFormat("Neplatná řádka: {0}", line);
                    }
                }
                while (line != null);

                theReader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }

    private void addKeyValue(string[] entries)
    {
        keys.Add(entries[0], entries[1]);
    }

    public string GetText(string key, bool checkPresence = true)
    {
        if (!isLoaded)
            throw new InvalidOperationException("translation wasn't loaded!");

        string res;
        var present = keys.TryGetValue(key, out res);

        if (checkPresence && !present)
            Debug.LogErrorFormat("Zadaný klíč >{0}< nebyl v překladech nalezen!", key);

        return res;
    }
}
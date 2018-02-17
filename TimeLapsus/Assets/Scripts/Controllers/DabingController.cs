using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

    class DabingController
    {
        public string SelectedAudioLanguage = "cz-phoenix";
        private readonly Dictionary<string, AudioClip> keysClipDictionary;
        private readonly bool isLoaded;

        private static DabingController _inst;

        public static DabingController Instance
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (_inst == null)
                    _inst = new DabingController();

                return _inst;
            }
        }

        private DabingController()
        {
            keysClipDictionary = new Dictionary<string, AudioClip>();
			var r = Resources.Load("translation", typeof(TextAsset)) as TextAsset;
			string text = r.text;
			if (String.IsNullOrEmpty (text)) {
				text = System.Text.Encoding.Default.GetString(r.bytes);
			}
            isLoaded = load(text);
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

                        if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(line.Trim()) || line.StartsWith("#"))
                            continue;

                        var entries = line.Trim().Split(new[] { '=' }, 2);
                        if (entries.Length == 2)
                        {
                            var key = entries[0];
                            loadDialog(key);
                        }
                        else
                        {
                            Debug.LogErrorFormat("Neplatná řádka: {0}", line);
                        }
                    }
                    while (line != null);

                    theReader.Close();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
            return true;
        }

        private void loadDialog(string key)
        {
            var dialogPath = Statics.AudioFolder + SelectedAudioLanguage + "/" + key;
            AudioClip dialog =  Resources.Load(dialogPath) as AudioClip;
       /*     if (dialog == null)
                Debug.LogErrorFormat("Audio file not found: {0}", dialogPath);*/
            keysClipDictionary.Add(key, dialog);
        }

        public AudioClip GetClip(string key, bool checkPresence = true)
        {
            if (!isLoaded)
                throw new InvalidOperationException("dialog files weren't correctly loaded!");

            AudioClip res;
            var present = keysClipDictionary.TryGetValue(key, out res);

            if (checkPresence && !present)
                Debug.LogErrorFormat("Zadaný dialog >{0}< nebyl v překladech nalezen!", key);

            return res;
        }
    }

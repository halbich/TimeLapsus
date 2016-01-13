using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DialogController : ScriptWithController
{
    public delegate void DialogEndAction();

    public Color MainCharacterLineColor = Color.black;
    public Color OtherCharacterLineColor = Color.black;

    public Color SpeakingCharacterHeadColor = Color.black;
    public Color OtherCharacterHeadColor = Color.black;

    private Dialog currentDialog;

    private readonly Color Transparent = new Color(0, 0, 0, 0);
    private Dictionary<string, Dialog> dialogs;
    private List<Dialog> randomDialogs;

    private static DialogController _inst;
    private GameObject panel;
    private Text textObject;
    private Image avatarImage;
    private Image avatarCharacterImage;
    private DialogueBlockerController dialogueBlocker;
    private bool isLoaded;

    public static DialogController Instance
    {
        get { return _inst ?? (_inst = FindObjectOfType<DialogController>()); }
    }

    // Use this for initialization
    protected override void Start()
    {
        _inst = null;
        panel = GameObject.FindGameObjectWithTag("DialogPanel");
        avatarImage = GameObject.FindGameObjectWithTag("DialogHead").GetComponent<Image>();
        avatarCharacterImage = GameObject.FindGameObjectWithTag("DialogHeadCharacter").GetComponent<Image>();
        dialogueBlocker = FindObjectOfType<DialogueBlockerController>();
        textObject = GetComponentInChildren<Text>();
        panel.SetActive(false);
        dialogueBlocker.Deactivate();
        isLoaded = createDialogs();
    }

    internal void ShowDialog(Dialog dialog, Sprite avatar = null, DialogEndAction endAction = null)
    {
        currentDialog = dialog;
        avatarImage.sprite = avatar;

        StartCoroutine(getAllLines(endAction));
    }

    private IEnumerator getAllLines(DialogEndAction endAction)
    {
        var speakActors = FindObjectsOfType<TalkingActor>().ToDictionary(e => e.EntityID, j => j);

        var dialog = currentDialog ?? new Dialog();

        var hasError = false;
        foreach (var actorID in dialog.DialogLines.Select(e => e.ActorID).Distinct().Where(actorID => !speakActors.ContainsKey(actorID)))
        {
            Debug.LogErrorFormat("Scene doesn't contains TalkingActor for {0}", actorID);
            hasError = true;
        }

        if (hasError)
            yield break;

        Controller.CursorManager.SetCursor();
        dialogueBlocker.Activate();
        dialogueBlocker.clicked = false;
        panel.SetActive(true);
        foreach (var item in dialog.DialogLines)
        {
            var currentActor = speakActors[item.ActorID];
            currentActor.StartSpeak();
            textObject.text = item.Text;
            if (item.ActorID == EnumActorID.MainCharacter)
            {
                textObject.color = MainCharacterLineColor;
                avatarCharacterImage.color = SpeakingCharacterHeadColor;
                avatarImage.color = avatarImage.sprite != null ? OtherCharacterHeadColor : Transparent;
                textObject.alignment = TextAnchor.UpperLeft;
            }
            else
            {
                textObject.color = OtherCharacterLineColor;
                avatarCharacterImage.color = OtherCharacterHeadColor;
                avatarImage.color = avatarImage.sprite != null ? SpeakingCharacterHeadColor : Transparent;
                textObject.alignment = TextAnchor.UpperRight;
            }

            dialogueBlocker.WaitForClick(Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => dialogueBlocker.clicked);
            dialogueBlocker.clicked = false;
            //yield return new WaitForSeconds(item.Duration);
            currentActor.EndSpeak();
        }

        panel.SetActive(false);
        dialogueBlocker.Deactivate();
        if (endAction != null)
        {
            endAction();
        }
    }

    internal Dialog GetDialog(string p)
    {
        if (!isLoaded)
            throw new InvalidOperationException("Dialogs are not correctly loaded!");

        if (string.IsNullOrEmpty(p))
            return null;

        p = p.Trim();

        Debug.LogFormat("Get dialog >{0}<", p);

        Dialog ret;
        if (dialogs.TryGetValue(p, out ret))
            return ret;

        Debug.LogErrorFormat("Zadaný dialog nebyl nalezen: >{0}<", p);
        return null;
    }

    private bool createDialogs()
    {
        dialogs = new Dictionary<string, Dialog>();
        randomDialogs = new List<Dialog>();
        var dialogRes = Resources.Load("dialogs") as TextAsset;

        return fillDialogs(dialogRes.text);
    }

    private bool fillDialogs(string data)
    {
        var ti = TextController.Instance;

        // Handle any problems that might arise when reading the text
        try
        {
            using (var theReader = new StringReader(data))
            {
                string line;
                do
                {
                    line = theReader.ReadLine();

                    if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(line.Trim()) || line.StartsWith("#"))
                        continue;

                    line = line.Trim();

                    var type = char.ToLower(line[0]);
                    var res = line.Substring(2);
                    switch (type)
                    {
                        case 's':
                            addSimpleDialog(res, ti);
                            break;

                        case 'd':
                            addDialog(res, ti);
                            break;

                        case 'r':
                            addRandomDialog(res, ti);
                            break;

                        default:
                            Debug.LogErrorFormat("Neplatný dialog: {0}", line);
                            break;
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

    private void addRandomDialog(string res, TextController ti)
    {
        var entries = res.Split(new[] { '=' }, 2);
        if (entries.Length != 2)
        {
            Debug.LogErrorFormat("Neplatná řádka: {0}", res);
            return;
        }

        var lines = entries[1].Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        randomDialogs.AddRange(
            from line in lines
            let actor = line.Substring(0, 4)
            let ln = line.Substring(4)
            select new Dialog
            {
                DialogLines = new List<DialogLine>
                {
                    new DialogLine(Statics.ActorMappings[actor], ti.GetText(ln))
                }
            });
    }

    private void addDialog(string res, TextController ti)
    {
        var entries = res.Split(new[] { '=' }, 2);
        if (entries.Length != 2)
        {
            Debug.LogErrorFormat("Neplatná řádka: {0}", res);
            return;
        }

        var lines = entries[1].Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        var dialogLines = new List<DialogLine>(lines.Length);
        dialogLines.AddRange(
            from line in lines
            let actor = line.Substring(0, 4)
            let ln = line.Substring(4)
            select new DialogLine(Statics.ActorMappings[actor], ti.GetText(ln)));

        dialogs.Add(entries[0], new Dialog
        {
            DialogLines = dialogLines
        });
    }

    private void addSimpleDialog(string res, TextController ti)
    {
        var entries = res.Split(new[] { '=' }, 2);
        if (entries.Length != 2)
        {
            Debug.LogErrorFormat("Neplatná řádka: {0}", res);
            return;
        }

        var lines = entries[1].Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        var dialogLines = new List<DialogLine>(lines.Length);
        dialogLines.AddRange(
            from line in lines
            let actor = line.Substring(0, 4)
            select new DialogLine(Statics.ActorMappings[actor], ti.GetText(entries[0])));

        dialogs.Add(entries[0], new Dialog
        {
            DialogLines = dialogLines
        });
    }

    internal void ShowRandomDialog()
    {
        var index = Random.Range(0, randomDialogs.Count);
        //  Debug.Log(index);
        ShowDialog(randomDialogs[index]);
    }
}
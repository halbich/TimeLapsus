using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public delegate void DialogEndAction();

    public Color MainCharacterLineColor = Color.black;
    public Color OtherCharacterLineColor = Color.black;

    public Color SpeakingCharacterHeadColor = Color.black;
    public Color OtherCharacterHeadColor = Color.black;

    private Dialog currentDialog;

    private readonly Color Transparent = new Color(0, 0, 0, 0);
    private Dictionary<string, Dialog> dialogs;

    private static DialogController _inst;
    private GameObject panel;
    private Text textObject;
    private Image avatarImage;
    private Image avatarCharacterImage;
    private DialogueBlockerController dialogueBlocker;
    public static DialogController Instance
    {
        get { return _inst ?? (_inst = FindObjectOfType<DialogController>()); }
    }

    // Use this for initialization
    private void Start()
    {
        _inst = null;
        panel = GameObject.FindGameObjectWithTag("DialogPanel");
        avatarImage = GameObject.FindGameObjectWithTag("DialogHead").GetComponent<Image>();
        avatarCharacterImage = GameObject.FindGameObjectWithTag("DialogHeadCharacter").GetComponent<Image>();
        dialogueBlocker = GameObject.FindObjectOfType<DialogueBlockerController>();
        textObject = GetComponentInChildren<Text>();
        panel.SetActive(false);
        dialogueBlocker.Deactivate();
        createDialogs();
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
        dialogueBlocker.Activate();
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
        if (string.IsNullOrEmpty(p))
            return null;

        Debug.LogFormat("Get dialog >{0}<", p);

        return dialogs[p];
    }

    private void createDialogs()
    {
        var ti = TextController.Instance;
        // this structure should be filled by some tool
        dialogs = new Dictionary<string, Dialog>();

        dialogs.Add("mayorFirst", new Dialog
        {
            DialogLines = new List<DialogLine>
            {
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p1")),
                new DialogLine(EnumActorID.Mayor, ti.GetText("m1")),
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p2")),
                new DialogLine(EnumActorID.Mayor, ti.GetText("m2")),
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p3"))
            }
        });

        dialogs.Add("potterFirst", new Dialog
        {
            DialogLines = new List<DialogLine>
            {
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p1")),
                new DialogLine(EnumActorID.Potter, ti.GetText("m1")),
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p2")),
                new DialogLine(EnumActorID.Potter, ti.GetText("m2")),
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p3"))
            }
        });



        addSimpleDialogs("mayorNotDisturb", "mayorBlueprints", "questDefinition", "inspectShovel", "hasShovel", "inspectVaseInventory", "vaseBuryingDialog",
            "potterNotDisturb", "needMoneyToBuy", "inspectGravePresent", "inspectGravePast", "inspectVase", "inspectPresentVaseBuried", "inspectPastVaseBuried");

        //Debug.LogFormat("Dialogů: {0}", dialogs.Count);
    }

    private void addSimpleDialogs(params string[] dialogNames)
    {
        foreach (var dialog in dialogNames)
        {
            dialogs.Add(dialog, Dialog.SimpleDialog(dialog));
        }
    }
}
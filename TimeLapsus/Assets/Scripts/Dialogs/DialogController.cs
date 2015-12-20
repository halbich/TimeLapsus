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

    public static DialogController Instance
    {
        get
        {
            if (_inst == null)
                _inst = FindObjectOfType<DialogController>();

            return _inst;
        }
    }

    // Use this for initialization
    private void Start()
    {
        _inst = null;
        panel = GameObject.FindGameObjectWithTag("DialogPanel");
        avatarImage = GameObject.FindGameObjectWithTag("DialogHead").GetComponent<Image>();
        avatarCharacterImage = GameObject.FindGameObjectWithTag("DialogHeadCharacter").GetComponent<Image>();
        textObject = GetComponentInChildren<Text>();
        panel.SetActive(false);

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
        var speakActors = GameObject.FindObjectsOfType<DialogActor>().ToDictionary(e => e.EntityID, j => j);

        panel.SetActive(true);
        foreach (var item in currentDialog.DialogLines)
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

            yield return new WaitForSeconds(item.Duration);
            currentActor.EndSpeak();
        }

        panel.SetActive(false);

        if (endAction != null)
        {
            endAction();
        }
    }

    internal Dialog GetDialog(string p)
    {
        return dialogs[p];
    }

    private void createDialogs()
    {
        var ti = TextController.Instance;
        // this structure should be filled by some tool
        dialogs = new Dictionary<string, Dialog>();

        dialogs.Add("mayorFirst", new Dialog()
        {
            DialogLines = new List<DialogLine>
            {
                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p1")),
                 new DialogLine(EnumActorID.Mayor, ti.GetText("m1")),
                                new DialogLine(EnumActorID.MainCharacter, ti.GetText("p2")),
                 new DialogLine(EnumActorID.Mayor, ti.GetText("m2")),
                 new DialogLine(EnumActorID.MainCharacter, ti.GetText("p3")),
            }
        }
            );
        dialogs.Add("mayorSecond", Dialog.SimpleDialog("mayorNotDisturb")
           );
        dialogs.Add("mayorBlueprints", Dialog.SimpleDialog("mayorBlueprints")
           );
    }
}
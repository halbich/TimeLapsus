using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class DialogController : MonoBehaviour
{

    public Color MainCharacterLineColor = Color.black;
    public Color OtherCharacterLineColor = Color.black;

    public Color SpeakingCharacterHeadColor = Color.black;
    public Color OtherCharacterHeadColor = Color.black;

    private Dialog currentDialog;

    private readonly Color Transparent = new Color(0, 0, 0, 0);






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
                _inst = GameObject.FindObjectOfType<DialogController>();

            return _inst;
        }
    }

    // Use this for initialization
    void Start()
    {
        _inst = null;
        panel = GameObject.FindGameObjectWithTag("DialogPanel");
        avatarImage = GameObject.FindGameObjectWithTag("DialogHead").GetComponent<Image>();
        avatarCharacterImage = GameObject.FindGameObjectWithTag("DialogHeadCharacter").GetComponent<Image>();
        textObject = GetComponentInChildren<Text>();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void ShowDialog(Dialog dialog, Sprite avatar)
    {

        currentDialog = dialog;
        avatarImage.sprite = avatar;

        StartCoroutine(getAllLines());
    }

    IEnumerator getAllLines()
    {

        var speakActors = GameObject.FindObjectsOfType<DialogActor>().ToDictionary(e => e.EntityID, j => j);

        panel.SetActive(true);
        foreach (var item in currentDialog.DialogLines)
        {
            var currentActor = speakActors[item.ActorID];
            currentActor.StartSpeak();
            textObject.text = item.Text;
            if(item.ActorID == EnumActorID.MainCharacter)
            {
                textObject.color = MainCharacterLineColor;
                avatarCharacterImage.color = SpeakingCharacterHeadColor;
                avatarImage.color =  avatarImage.sprite != null ? OtherCharacterHeadColor : Transparent;
            }
            else
            {
                textObject.color = OtherCharacterLineColor;
                avatarCharacterImage.color = OtherCharacterHeadColor;
                avatarImage.color = avatarImage.sprite != null ? SpeakingCharacterHeadColor : Transparent;
            }


            yield return new WaitForSeconds(item.Duration);
            currentActor.EndSpeak();
        }

        panel.SetActive(false);
    }
}

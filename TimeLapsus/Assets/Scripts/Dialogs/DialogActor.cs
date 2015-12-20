using System.Linq;
using UnityEngine;

public class DialogActor : TalkingActorWithController
{

    public bool ShowSpeakIcon = true;





    private SpeakPoint SpeakerPoint;


    private void OnMouseEnter()
    {
        Controller.CursorManager.SetCursor(CursorType.Speak);
    }

    private void OnMouseExit()
    {
        Controller.CursorManager.SetCursor();
    }

    private void OnMouseDown()
    {
        if (SpeakerPoint != null)

            Controller.PlayerController.MoveTo(SpeakerPoint.StartPoint, Speak);
        else
            Speak();
    }

    protected override void Start()
    {
        base.Start();

        var comps = FindObjectsOfType<SpeakPointScript>().SingleOrDefault(e => e.BelongsToActor == EntityID);
        if (comps == null)
            Debug.LogErrorFormat("No speak point defined for {0}! ", gameObject.name);
        else
        {
            SpeakerPoint = comps.GetPoint(Controller.CharacterZPosition);
            Destroy(comps);
        }

    }
}

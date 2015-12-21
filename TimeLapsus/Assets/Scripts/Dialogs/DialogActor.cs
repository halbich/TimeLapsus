using System.Linq;
using UnityEngine;

public class DialogActor : TalkingActorWithController
{

    public bool ShowSpeakIcon = true;

    DialogActor()
    {
        cursor = CursorType.Speak;
    }



    private SpeakPoint SpeakerPoint;
    

    private void OnMouseDown()
    {
        if (IsOverUI()) return;
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

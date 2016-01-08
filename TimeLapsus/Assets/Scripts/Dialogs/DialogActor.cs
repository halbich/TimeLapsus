using UnityEngine;

public class DialogActor : TalkingActorWithController
{
    public bool ShowSpeakIcon = true;

    private DialogActor()
    {
        cursor = CursorType.Speak;
    }

    private DirectionPoint SpeakerPoint;

    private void OnMouseDown()
    {
        if (IsOverUI()) return;
        if (SpeakerPoint != null)

            Controller.PlayerController.MoveTo(SpeakerPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(SpeakerPoint.Direction);
                Speak();
            });
        else
            Speak();
    }

    protected override void Start()
    {
        base.Start();

        var comps = GetComponentInChildren<ItemPointScript>();
        if (comps == null)
            Debug.LogErrorFormat("No speak point defined for {0}! ", gameObject.name);
        else
        {
            SpeakerPoint = comps.GetPoint(Controller.CharacterZPosition);
        }
    }
}
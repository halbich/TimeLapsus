using UnityEngine;

public class PotterDialogActor : TalkingActorWithController
{
    private bool fistFlag;

    public bool ShowSpeakIcon = true;

    private PotterDialogActor()
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

    internal void SetFistFlag()
    {
        fistFlag = true;
    }

    internal override void StartSpeak()
    {
        if (!fistFlag)
        {
            base.StartSpeak();
            return;
        }

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("FistStart");

    }

    internal override void EndSpeak()
    {
        if (!fistFlag)
        {
            base.EndSpeak();
            return;
        }

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("FistEnd");

        fistFlag = false;
    }
}
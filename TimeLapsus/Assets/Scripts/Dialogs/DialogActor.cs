using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DialogActor : ScriptWithController
{

    public bool ShowSpeakIcon = true;

    public EnumActorID EntityID;

    public Sprite Avatar;

    private SpeakPoint? SpeakerPoint;

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
        if (SpeakerPoint.HasValue)

            Controller.PlayerController.MoveTo(SpeakerPoint.Value.StartPoint, () =>
            {
                QuestController.Instance.Speak(EntityID, Avatar);
            });
        else
            QuestController.Instance.Speak(EntityID, Avatar);
    }

    internal void StartSpeak()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("SpeakStart");
    }

    internal void EndSpeak()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("SpeakEnd");
    }


    protected override void Start()
    {
        base.Start();

        var aa = GameObject.FindObjectsOfType<SpeakPointScript>();


        var comps = GameObject.FindObjectsOfType<SpeakPointScript>().SingleOrDefault(e => e.BelongsToActor == EntityID);
        if (comps == null)
            Debug.LogErrorFormat("No speak point defined for {0}! ", gameObject);
        else
        {
            SpeakerPoint = comps.GetPoint(Controller.CharacterZPosition);
            Destroy(comps);
        }
    }
}

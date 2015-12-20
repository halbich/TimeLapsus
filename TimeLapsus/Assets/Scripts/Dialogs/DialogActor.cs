using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class DialogActor : ScriptWithController
{

    public bool ShowSpeakIcon = true;

    public EnumActorID EntityID;

    public Sprite Avatar;

    private SpeakPoint SpeakerPoint;

    private DialogActorController dialogController;

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
        if (SpeakerPoint!= null)

            Controller.PlayerController.MoveTo(SpeakerPoint.StartPoint, () =>
            {
               // QuestController.Instance.Speak(EntityID, Avatar);
                dialogController.Speak();
            });
        else
            dialogController.Speak();
            //QuestController.Instance.Speak(EntityID, Avatar);
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

        var comps = FindObjectsOfType<SpeakPointScript>().SingleOrDefault(e => e.BelongsToActor == EntityID);
        if (comps == null)
            Debug.LogErrorFormat("No speak point defined for {0}! ", gameObject.name);
        else
        {
            SpeakerPoint = comps.GetPoint(Controller.CharacterZPosition);
            Destroy(comps);
        }

        dialogController = GetComponent<DialogActorController>();
        if(dialogController == null)
            Debug.LogErrorFormat("No dialogActorComponent defined for {0}! ", gameObject.name);
        else 
            dialogController.SetAvatar(Avatar);
    }
}

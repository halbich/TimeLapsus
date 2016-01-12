using UnityEngine;

public class TalkingActor : ClickableArea
{
    public EnumActorID EntityID;

    internal virtual void StartSpeak()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("SpeakStart");
    }

    internal virtual void EndSpeak()
    {
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("SpeakEnd");
    }
}
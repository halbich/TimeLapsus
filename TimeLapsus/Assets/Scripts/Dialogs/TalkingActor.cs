using UnityEngine;

public class TalkingActor : ScriptWithController
{
    public EnumActorID EntityID;
  


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


}

using UnityEngine;

public class MayorDialogActor : DialogActor
{
    private bool flag;

    internal void SetFlag()
    {
        flag = true;
    }

    internal override void StartSpeak()
    {
        if (!flag)
        {
            base.StartSpeak();
            return;
        }

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("HappySpeakStart");

    }

    internal override void EndSpeak()
    {
        if (!flag)
        {
            base.EndSpeak();
            return;
        }

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("HappySpeakEnd");

        flag = false;
    }
}
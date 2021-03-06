﻿using UnityEngine;

public class PotterDialogActor : DialogActor
{
    private bool fistFlag;

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
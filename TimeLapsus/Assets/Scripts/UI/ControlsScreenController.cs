using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ControlsScreenController : ScriptWithController {
    public bool ShowingTutorial = false;
    Image tutorialImage = null;
    Animator usedAnimator;
    bool pauseOnAnimFinish = false;
    // Update is called once per frame

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        tutorialImage = GetComponent<Image>();
        usedAnimator = GetComponent<Animator>();
    }
    void Update ()
    {
        if ( usedAnimator.GetCurrentAnimatorStateInfo(0).IsName("ControlsScreenVisible") && pauseOnAnimFinish)
        {
            Time.timeScale = 0;
            pauseOnAnimFinish = false;
        }
    }
    public void ToggleTutorial() {
        if (ShowingTutorial) HideTutorial();
        else ShowTutorial();
    }
    public void ShowTutorial()
    {
        ShowingTutorial = true;
        pauseOnAnimFinish = true;
        Controller.CursorManager.SetCursor();
        Controller.DisableInput();
        usedAnimator.SetTrigger("ShowScreen");
    }

    public void HideTutorial()
    {
        ShowingTutorial = false;
        Time.timeScale = 1;
        Controller.EnableInput();
        usedAnimator.SetTrigger("HideScreen");
    }
}

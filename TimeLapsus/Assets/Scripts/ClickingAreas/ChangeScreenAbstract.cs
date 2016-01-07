using System.Collections;
using UnityEngine;

public abstract class ChangeScreenAbstract : ClickableArea
{

    protected bool setTimeLineChangedValue;

    public ChangeScreenAbstract()
    {
        cursor = CursorType.GoToLocationN;
    }

    protected virtual void Change(EnumLevel level)
    {
        foreach (var musicController in FindObjectsOfType<AmbientMusicController>())
        {
            musicController.QuietDown(1.5f);
        }

        Statics.TimelineChanged = setTimeLineChangedValue;


        StartCoroutine(ChangeCor(level));
    }

    public EnumLevel Level;

    private void OnMouseDown()
    {
        // we dont want to have clicks on disabled scripts
        // http://answers.unity3d.com/questions/19671/disabled-script-still-does-onmousedown.html

        if (enabled && IsInBox && !IsOverUI())
        {
            Controller.PlayerController.MoveTo(Controller.GetEnterPosition(Level), () =>
            {
                Change(Level);
            });
        }
    }

    private IEnumerator ChangeCor(EnumLevel level)
    {
        if (Statics.TimelineChanged)
        {
            var screenChanger = FindObjectOfType<ScreenChanger>();
            screenChanger.Activate();
            yield return new WaitUntil(() => screenChanger.CanContinueWithLoad);
        }
        else
        {


            if (Controller.Fader != null)
            {
                Controller.Fader.EndScene();
                yield return new WaitForSeconds(1);
                yield return new WaitUntil(() => Controller.Fader.CurrentState != FadeInOutState.None);
            }
        }

        Controller.ChangeScene(level);
    }
}
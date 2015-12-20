using System.Collections;
using UnityEngine;

public class ChangeScreen : ClickableArea
{
    public ChangeScreen()
    {
        cursor = CursorType.GoToLocationS;
    }

    protected virtual void Change(EnumLevel level)
    {
        foreach (var musicController in FindObjectsOfType<AmbientMusicController>())
        {
            musicController.QuietDown(1.5f);
        }


        StartCoroutine(ChangeCor(level));

    }


    public EnumLevel Level;

    private void OnMouseDown()
    {
        if (IsInBox)
        {
            Controller.PlayerController.MoveTo(Controller.GetEnterPosition(Level), () =>
            {
                Change(Level);
            });

        }
    }


    IEnumerator ChangeCor(EnumLevel level)
    {
        if (Controller.Fader != null)
        {
            Controller.Fader.EndScene();
            yield return new WaitForSeconds(2);
        }

        Controller.ChangeScene(level);
    }
}

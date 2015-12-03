using UnityEngine;
using System.Collections;

public class ChangeTimeLine : ChangeScreen
{
    public ChangeTimeLine() : base()
    {
        cursor = CursorType.GoToLocation;
    }
    protected override void Change(EnumLevel level)
    {

        StartCoroutine(ChangeCor(level));

    }

    IEnumerator ChangeCor(EnumLevel level)
    {
        if (Controller.Fader != null)
        {
            Controller.Fader.EndScene();
            yield return new WaitForSeconds(2);
        }

        base.Change(level);
    }


}

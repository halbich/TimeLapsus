
using UnityEngine;

public class ChangeTimeLine : ChangeScreenAbstract
{
    public ChangeTimeLine()
    {
        cursor = CursorType.GoToLocationS;
        setTimeLineChangedValue = true;
    }

    internal void SetIsInBox(bool value)
    {
        IsInBox = value;
    }
}
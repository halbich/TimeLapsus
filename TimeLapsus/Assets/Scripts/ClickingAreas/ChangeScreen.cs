﻿using UnityEngine;
using System.Collections;

public class ChangeScreen : ClickableArea
{

    protected virtual void Change(EnumLevel level)
    {
        Controller.ChangeScene(level);
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
}

using UnityEngine;
using System.Collections;

public class ChangeScreen : ClickableArea
{



    public EnumLevel Level;

    private void OnMouseDown()
    {
        if (IsInBox)
        {
            Controller.PlayerController.MoveTo(Controller.GetEnterPosition(Level), () =>
            {
               Controller.ChangeScene(Level);


            });

        }
    }
}

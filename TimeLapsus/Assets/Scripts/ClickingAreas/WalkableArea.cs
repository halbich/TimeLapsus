using UnityEngine;

public class WalkableArea : ClickableArea
{
    public WalkableArea() : base()
    {
        cursor = CursorType.Walk;
    }

    private void OnMouseDown()
    {
        if (IsInBox)
        {
            var v = Input.mousePosition;
            var point = Camera.main.ScreenToWorldPoint(v);
         



            Controller.PlayerController.MoveTo(point, () =>
            {
              

            });
            //  PC.transform.position.Set(PC.transform.position.x, PC.transform.position.y, -5);
        }
    }

    

    // Update is called once per frame
    private void Update()
    {
    }

   
}
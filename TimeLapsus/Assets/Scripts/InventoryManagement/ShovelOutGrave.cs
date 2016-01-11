using UnityEngine;

public class ShovelOutGrave : ItemUseOnScript
{
    public string ShoveledGraveKeyName;

    public override void Use()
    {
        if (ObjectPoint == null)
        {
            Debug.LogError("No object point defined!");
            return;
        }


        Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
        {
            Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
            base.Use();
            QuestController.Instance.GetCurrent().SetBoolean(ShoveledGraveKeyName);
            var c = GetComponent<GravePresentController>();
            c.ActionOccured();
        });

       
    }
}
using UnityEngine;

public class UseVaseOnLara : ItemUseOnScript
{
    public string InsertedVaseVarName;
    public string VaseInsertDialog;
    public DialogActor Lara;

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
            //QuestController.Instance.GetCurrent().SetBoolean(InsertedVaseVarName);
            var dialogController = DialogController.Instance;
            dialogController.ShowDialog(dialogController.GetDialog(VaseInsertDialog), Lara.Avatar );
            base.Use();
        });
    }
}
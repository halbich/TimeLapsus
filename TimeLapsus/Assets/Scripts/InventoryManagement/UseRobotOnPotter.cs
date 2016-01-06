using UnityEngine;

public class UseRobotOnPotter : ItemUseOnScript
{
    public string PotterIsDeadVarName;
    public string AfterPotterDeadDialog;

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
            QuestController.Instance.GetCurrent().SetBoolean(PotterIsDeadVarName);
            var dialogController = DialogController.Instance;
            dialogController.ShowDialog(dialogController.GetDialog(AfterPotterDeadDialog));
            base.Use();
            Destroy(gameObject);
        });
    }
}
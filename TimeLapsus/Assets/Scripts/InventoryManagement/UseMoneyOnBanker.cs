using UnityEngine;

public class UseMoneyOnBanker : ItemUseOnScript
{
    public string SetAccountHasMoneyVarName;
    public string UseMoneyDialog;

    public override void Use()
    {
        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                QuestController.Instance.GetCurrent().SetBoolean(SetAccountHasMoneyVarName);
                var dialogController = DialogController.Instance;
                dialogController.ShowDialog(dialogController.GetDialog(UseMoneyDialog), GetComponent<DialogActor>().Avatar);
                base.Use();
            });
        else Debug.LogError("No object point defined!");
    }
}


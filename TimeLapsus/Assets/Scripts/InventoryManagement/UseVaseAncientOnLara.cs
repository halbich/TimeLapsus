using UnityEngine;

public class UseVaseAncientOnLara : ItemUseOnScript
{
    public string InsertedVaseVarName;
    public string VaseInsertDialog;

    public DialogActor Lara;
    public LaraExpoController expo;
    public TakeCoins coins;

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

            var laraController = GetComponent<LaraSpeakController>();
            var quest = QuestController.Instance.GetCurrent();

            quest.SetBoolean(laraController.LaraHasVaseOnTableVarName);
            var dialogController = DialogController.Instance;
            dialogController.ShowDialog(dialogController.GetDialog(VaseInsertDialog), Lara.Avatar, () =>
            {
                quest.SetBoolean(laraController.LaraHasVaseOnTableVarName, false);
                quest.SetBoolean(laraController.LaraHasMoneyOnTable);
                quest.SetBoolean(coins.InspectedItemVariable);
                coins.GetComponent<PickableItem>().SetInspected();
                laraController.UpdateItems();
                quest.SetBoolean(expo.VaseIsInExpoVarName);
                expo.UpdateItems();
            });

            laraController.UpdateItems();

            base.Use();
        });
    }
}
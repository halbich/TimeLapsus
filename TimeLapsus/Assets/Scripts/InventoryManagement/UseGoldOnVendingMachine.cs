public class UseGoldOnVendingMachine : ItemUseOnScript
{
    public VendingMachineController controller;

    public override void Use()
    {
        //if (ObjectPoint != null)

        //    Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
        //    {
        //        Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
        //        QuestController.Instance.GetCurrent().SetBoolean("buriedVase");
        //        var dialogController = DialogController.Instance;
        //        dialogController.ShowDialog(dialogController.GetDialog("vaseBuryingDialog"));
        //        base.Use();
        //    });

        if (controller != null && controller.ChipObject != null)
            controller.ChipObject.SetActive(true);

        base.Use();
    }
}
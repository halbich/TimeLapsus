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

        if (controller.ChipObject == null)
            return;

        controller.ChipObject.SetActive(true);

        var quest = QuestController.Instance.GetCurrent();
        quest.SetBoolean(controller.ChipWasGivenVarName);
        quest.SetBoolean(controller.ChipObject.GetComponent<InspectObjectController>().InspectedItemVariable);
        controller.ChipObject.GetComponent<PickableItem>().SetInspected();

        base.Use();
    }
}
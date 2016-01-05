namespace Assets.Scripts.InventoryManagement
{
    internal class UseVaseOnGravePastscript : ItemUseOnScript
    {
        public override void Use()
        {
            if (ObjectPoint != null)

                Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
                {
                    Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                    QuestController.Instance.GetCurrent().SetBoolean("buriedVase");
                    var dialogController = DialogController.Instance;
                    dialogController.ShowDialog(dialogController.GetDialog("vaseBuryingDialog"));
                    base.Use();
                    //QuestController.Instance.Inspect(EntityID);
                });
        }
    }
}
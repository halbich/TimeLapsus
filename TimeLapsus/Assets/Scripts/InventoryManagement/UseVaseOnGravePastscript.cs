using UnityEngine;

namespace Assets.Scripts.InventoryManagement
{
    public class UseVaseOnGravePastscript : ItemUseOnScript
    {
        public string InsertedVaseVarName;
        public string VaseInsertDialog;

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
                QuestController.Instance.GetCurrent().SetBoolean(InsertedVaseVarName);
                var dialogController = DialogController.Instance;
                dialogController.ShowDialog(dialogController.GetDialog(VaseInsertDialog));
                GetComponent<GravePastController>().ActionOccured();
                base.Use();
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.InventoryManagement
{
    class UseVaseOnGravePastscript : ItemUseOnScript
    {
        public override void Use()
        {
            if (ObjectPoint != null)

                Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
                {
                    Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                    QuestController.Instance.GetCurrent().SetValue("buriedVase", true);
                    var dialogController = DialogController.Instance;
                    dialogController.ShowDialog(dialogController.GetDialog("vaseBuryingDialog"));
                    base.Use();
                    //QuestController.Instance.Inspect(EntityID);
                });
        }
    }
}

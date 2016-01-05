using UnityEngine;
using System.Collections;

public class DigVaseIntoGrave : ItemUseOnScript
{
    public string BuriedVaseVarName;
    public string VaseBuryDialog;
    public override void Use()
    {
        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                QuestController.Instance.GetCurrent().SetBoolean(BuriedVaseVarName);
                var dialogController = DialogController.Instance;
                dialogController.ShowDialog(dialogController.GetDialog(VaseBuryDialog));
                GetComponent<GravePastController>().ActionOccured();
                base.Use();
            });
    }
}

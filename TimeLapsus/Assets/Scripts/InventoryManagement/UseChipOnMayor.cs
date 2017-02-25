using UnityEngine;

public class UseChipOnMayor : ItemUseOnScript
{
    public DialogActor RepresentedDialog;
    public override void Use()
    {
        RepresentedDialog.StartDialog();
    }
}
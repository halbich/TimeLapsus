using UnityEngine;
using System.Collections;

public class AfterTeleportTrigger : DialogActorController
{
    private const string HasSpoken = "hasTriggeredAfterTeleportDialog";

    protected override string getDialog()
    {
        //pokud jsem nezobrazil popis questu

        if(currentQuest.GetBoolean(HasSpoken))
        {
            return null;
        }

        return "teleportPostInfo";
    }

    protected override void endDialogAction()
    {
        currentQuest.SetBoolean(HasSpoken);
    }
}
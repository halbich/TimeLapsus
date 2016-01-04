using UnityEngine;
using System.Collections;

public class AfterTeleportTrigger : DialogActorController
{
    private const string HasSpoken = "hasTriggeredAfterTeleportDialog";

    protected override string getDialog()
    {
        //pokud jsem nezobrazil popis questu

        bool hasAlreadySpeaked;
        if (currentQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return null;
        }

        return "teleportPostInfo";
    }

    protected override void endDialogAction()
    {
        currentQuest.SetValue(HasSpoken, true);
    }
}
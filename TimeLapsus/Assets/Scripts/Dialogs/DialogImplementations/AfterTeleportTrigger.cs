public class AfterTeleportTrigger : DialogActorController
{
    private const string HasSpoken = "hasTriggeredAfterTeleportDialog";

    public string TeleportPostInfo;

    protected override string getDialog()
    {
        //pokud jsem nezobrazil popis questu

        if (currentQuest.GetBoolean(HasSpoken))
        {
            return null;
        }

        return TeleportPostInfo;
    }

    protected override void endDialogAction()
    {
        if (!currentQuest.GetBoolean(HasSpoken))
            currentQuest.SetBoolean(HasSpoken);
    }
}
public class RiverTriggerController : DialogActorController
{
    private const string HasSpoken = "hasTriggeredRiver";

    protected override string getDialog()
    {
        //pokud jsem nezobrazil popis questu

        if (currentQuest.GetBoolean(HasSpoken))
        {
            return null;
        }

        return "questDefinition";
    }

    protected override void endDialogAction()
    {
        currentQuest.SetBoolean(HasSpoken);
    }
}
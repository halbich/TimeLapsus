public class RiverTriggerController : DialogActorController
{

    private const string HasSpoken = "hasTriggeredRiver";

    protected override string getDialog()
    {


        //pokud jsem nezobrazil popis questu

        bool hasAlreadySpeaked;
        if (currentQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return null;
        }

        return "questDefinition";
    }


    protected override void endDialogAction()
    {
      currentQuest.SetValue(HasSpoken, true);
    }
}

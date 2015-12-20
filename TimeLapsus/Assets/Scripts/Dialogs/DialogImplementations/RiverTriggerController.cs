public class RiverTriggerController : DialogActorController
{

    private const string HasSpoken = "hasTriggeredRiver";

    protected override string getDialog()
    {
        var cQuest = QuestController.Instance.GetCurrent();


        //pokud jsem nezobrazil popis questu

        bool hasAlreadySpeaked;
        if (cQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return null;
        }

        return "questDefinition";
    }


    protected override void endDialogAction()
    {
        var cQuest = QuestController.Instance.GetCurrent();
        cQuest.SetValue(HasSpoken, true);
    }
}

public class BankerPresentSpeakController : DialogActorController
{

    private const string HasSpoken = "hasSpokenWithLara";

    protected override string getDialog()
    {
        var cQuest = QuestController.Instance.GetCurrent();
      
        
        //pokud mám čip, mluv
        
        //bool hasAlreadySpeaked;
        //if (cQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        //{
        //    return "mayorNotDisturb";
        //}
        //else
        //{
        //    return "mayorFirst";
        //}
        return null;
    }


    protected override void endDialogAction()
    {
        var cQuest = QuestController.Instance.GetCurrent();
        cQuest.SetValue(HasSpoken, true);
    }
}

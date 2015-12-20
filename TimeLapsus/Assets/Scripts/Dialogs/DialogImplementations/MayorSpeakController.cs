public class MayorSpeakController : DialogActorController
{

    private const string HasSpoken = "hasSpokenWithMayor";

    protected override string getDialog()
    {
        var cQuest = QuestController.Instance.GetCurrent();
      
        
        //pokud mám čip, mluv
        
        bool hasAlreadySpeaked;
        if (cQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return "mayorNotDisturb";
        }
        return "mayorFirst";
    }


    protected override void endDialogAction()
    {
        var cQuest = QuestController.Instance.GetCurrent();
        cQuest.SetValue(HasSpoken, true);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
}

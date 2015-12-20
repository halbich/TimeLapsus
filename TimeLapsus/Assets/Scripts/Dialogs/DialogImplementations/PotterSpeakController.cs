public class PotterSpeakController : DialogActorController
{

    private const string HasSpoken = "hasSpokenWithPotter";

    protected override string getDialog()
    {
        var cQuest = QuestController.Instance.GetCurrent();
        bool hasAlreadySpeaked;
        if (cQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return "potterNotDisturb";
        }
        return "potterFirst";
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

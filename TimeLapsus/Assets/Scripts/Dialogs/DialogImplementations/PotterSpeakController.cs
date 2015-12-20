public class PotterSpeakController : DialogActorController
{

    private const string HasSpoken = "hasSpokenWithPotter";

    protected override string getDialog()
    {
        bool hasAlreadySpeaked;
        if (currentQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return "potterNotDisturb";
        }
        return "potterFirst";
    }


    protected override void endDialogAction()
    {
        currentQuest.SetValue(HasSpoken, true);
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

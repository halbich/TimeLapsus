public class MayorSpeakController : DialogActorController
{
    private const string HasSpoken = "hasSpokenWithMayor";

    protected override string getDialog()
    {
        bool hasAlreadySpeaked;
        if (currentQuest.TryGetValue(HasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            return "mayorNotDisturb";
        }
        return "mayorFirst";
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
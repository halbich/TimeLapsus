public class MayorSpeakController : DialogActorController
{
    private const string HasSpoken = "hasSpokenWithMayor";

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(HasSpoken))
        {
            return "mayorNotDisturb";
        }
        return "mayorFirst";
    }

    protected override void endDialogAction()
    {
        currentQuest.SetBoolean(HasSpoken);
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
public class PotterSpeakController : DialogActorController
{
    private const string HasSpoken = "hasSpokenWithPotter";

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(HasSpoken))
        {
            return "potterNotDisturb";
        }
        return "potterFirst";
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
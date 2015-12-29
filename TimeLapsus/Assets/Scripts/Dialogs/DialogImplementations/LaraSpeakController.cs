public class LaraSpeakController : DialogActorController
{
    private const string HasSpoken = "hasSpokenWithLara";

    protected override string getDialog()
    {
        return null;
    }

    protected override void endDialogAction()
    {
        currentQuest.SetValue(HasSpoken, true);
    }
}
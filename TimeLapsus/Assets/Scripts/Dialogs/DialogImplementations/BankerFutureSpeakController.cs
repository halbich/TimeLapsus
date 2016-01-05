public class BankerFutureSpeakController : DialogActorController
{
    public string HasAccountKeyName;
    public string BankerNoAccountDialog;
    public string BankerNoAccountDialog2;


    private string hasSpokenWithBanker = "hasSpokenWithBankerFuture";


    protected override string getDialog()
    {
        if (!currentQuest.GetBoolean(HasAccountKeyName))
        {
            return currentQuest.GetBoolean(hasSpokenWithBanker) ? BankerNoAccountDialog2 : BankerNoAccountDialog;
        }

        return null;
    }

    protected override void endDialogAction()
    {
        if (!currentQuest.GetBoolean(hasSpokenWithBanker))
            currentQuest.SetBoolean(hasSpokenWithBanker);
    }
}
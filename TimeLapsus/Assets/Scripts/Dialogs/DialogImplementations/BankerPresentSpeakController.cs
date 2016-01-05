public class BankerPresentSpeakController : DialogActorController
{
    public string HasAccountKeyName;

    public string BankerIntroDialog;
    public string NoMoneyRepeatDialog;

    public string HasMoneyRepeat;

    public UseMoneyOnBanker MoneyScript;


    private string hasSpokenWithBanker = "hasSpokenWithBankerFuture";

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(MoneyScript.SetAccountHasMoneyVarName))
            return HasMoneyRepeat;


        return currentQuest.GetBoolean(hasSpokenWithBanker) ? NoMoneyRepeatDialog: BankerIntroDialog;
    }

    protected override void endDialogAction()
    {
        if (!currentQuest.GetBoolean(hasSpokenWithBanker))
            currentQuest.SetBoolean(hasSpokenWithBanker);

        if (!currentQuest.GetBoolean(HasAccountKeyName))
            currentQuest.SetBoolean(HasAccountKeyName);
    
    }
}
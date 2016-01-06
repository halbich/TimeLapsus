using UnityEngine;

public class BankerFutureSpeakController : DialogActorController
{
    public string HasAccountKeyName;
    public string BankerNoAccountDialog;
    public string BankerNoAccountDialog2;

    public string InitialMoneyKeyName;
    public string BankerTakeMoneyDialog;

    public string AccountEmptyDialog;


    public string HasGivenIngotKeyName;
    public GameObject GoldIngot;

    private string hasSpokenWithBanker = "hasSpokenWithBankerFuture";

    protected override void Start()
    {
        base.Start();
        GoldIngot.SetActive(currentQuest.GetBoolean(HasGivenIngotKeyName) && !currentQuest.GetBoolean(GoldIngot.GetComponent<PickableItem>().pickedUpItemVariable));
    }

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(HasGivenIngotKeyName))
        {
            return AccountEmptyDialog;
        }

        if (!currentQuest.GetBoolean(HasAccountKeyName))
        {
            return currentQuest.GetBoolean(hasSpokenWithBanker) ? BankerNoAccountDialog2 : BankerNoAccountDialog;
        }


        return !currentQuest.GetBoolean(InitialMoneyKeyName) ? AccountEmptyDialog : BankerTakeMoneyDialog;
    }

    protected override void endDialogAction()
    {
        if (!currentQuest.GetBoolean(hasSpokenWithBanker))
            currentQuest.SetBoolean(hasSpokenWithBanker);

        if (!currentQuest.GetBoolean(HasAccountKeyName) ||
            !currentQuest.GetBoolean(InitialMoneyKeyName) ||
        currentQuest.GetBoolean(HasGivenIngotKeyName))
            return;

        currentQuest.SetBoolean(HasGivenIngotKeyName);
        var inspConst = GoldIngot.GetComponent<InspectObjectController>();
        currentQuest.SetBoolean(inspConst.InspectedItemVariable);
        GoldIngot.GetComponent<PickableItem>().SetInspected();

        GoldIngot.SetActive(true);
    }
}

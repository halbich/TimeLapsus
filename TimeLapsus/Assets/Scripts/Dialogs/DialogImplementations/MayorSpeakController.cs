public class MayorSpeakController : DialogActorController
{
    public string HasChipFirstDialog;
    public string NoChipFirstDialog;
    public string NoChipNoDisturb;
    public string HasChipNowDialog;
    public string CreateBridgeVarName;
    public string BridgeCreatedDialog;

    private const string HasSpoken = "hasSpokenWithMayor";

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(CreateBridgeVarName))
            return BridgeCreatedDialog;

        if (Controller.HasInventoryItem(EnumItemID.Chip))
        {
            return currentQuest.GetBoolean(HasSpoken) ? HasChipNowDialog : HasChipFirstDialog;
        }

        return currentQuest.GetBoolean(HasSpoken) ? NoChipNoDisturb : NoChipFirstDialog;
    }

    protected override void endDialogAction()
    {
        if (Controller.HasInventoryItem(EnumItemID.Chip))
        {
            currentQuest.SetBoolean(CreateBridgeVarName);
            return;
            
        }

        currentQuest.SetBoolean(HasSpoken);

        base.endDialogAction();
    }

}
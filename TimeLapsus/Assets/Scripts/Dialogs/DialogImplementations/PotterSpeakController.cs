public class PotterSpeakController : DialogActorController
{
    private const string HasSpoken = "hasSpokenWithPotter";

    public string NoChipPotterFirst;
    public string NoChipPotterNoDisturb;

    public UseRobotOnPotter ActionObject;

    protected override void Start()
    {
        base.Start();
        if (currentQuest.GetBoolean(ActionObject.PotterIsDeadVarName))
        {
            Destroy(ActionObject.R2D2);
            Destroy(ActionObject.Hider);
            Destroy(gameObject);
            

        }
    }

    protected override string getDialog()
    {
        return currentQuest.GetBoolean(HasSpoken) ? NoChipPotterNoDisturb : NoChipPotterFirst;
    }

    protected override void endDialogAction()
    {
        if (!currentQuest.GetBoolean(HasSpoken))
            currentQuest.SetBoolean(HasSpoken);
    }
}
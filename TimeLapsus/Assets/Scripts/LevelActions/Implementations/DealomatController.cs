using UnityEngine;

public class DealomatController : InspectObjectController
{
    public BankerFutureSpeakController banker;

    public string NoAccountDialog;
    public string AlreadyHasRobotDialog;
    public string BeforeRobotTakeDialog;

    public string RobotReadyToTakeVarName;

    public string HasRobotVarName;
    public GameObject R2D2;

    private bool beforeTakenTriggered;

    protected override void Start()
    {
        base.Start();

        if (R2D2 == null)
            return;

        R2D2.SetActive(currentQuest.GetBoolean(RobotReadyToTakeVarName));
    }

    protected override string getDialog()
    {
        if (!IsInspected())
            return base.getDialog();

        if (Controller.HasInventoryItem(EnumItemID.Robot) || currentQuest.GetBoolean(banker.InitialMoneyKeyName))
            return AlreadyHasRobotDialog;

        if (!currentQuest.GetBoolean(banker.HasAccountKeyName))
            return NoAccountDialog;

        beforeTakenTriggered = true;
        return BeforeRobotTakeDialog;
    }

    protected override void endDialogAction()
    {
        base.endDialogAction();

        if (beforeTakenTriggered && !currentQuest.GetBoolean(RobotReadyToTakeVarName))
        {
            beforeTakenTriggered = false;
            currentQuest.SetBoolean(RobotReadyToTakeVarName);
            if (R2D2 != null)
                R2D2.SetActive(true);
        }
    }
}
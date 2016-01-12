using UnityEngine;

public class DealomatController : InspectObjectController
{

    public DealomatController()
    {
        canLoadHeadImage = true;
    }

    public BankerFutureSpeakController banker;

    public string NoAccountDialog;
    public string AlreadyHasRobotDialog;
    public string BeforeRobotTakeDialog;

    public string RobotReadyToTakeVarName;

    public string HasRobotVarName;


    private bool beforeTakenTriggered;

    private Animator anim;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();

        if (currentQuest.GetBoolean(RobotReadyToTakeVarName))
        {
            anim.SetFloat("Speed", 1000000);
        }
    }

    protected override string getDialog()
    {
        if (!IsInspected())
            return base.getDialog();

        if (Controller.HasInventoryItem(EnumItemID.Robot) || currentQuest.GetBoolean(RobotReadyToTakeVarName) || currentQuest.GetBoolean(banker.InitialMoneyKeyName))
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

            anim.SetFloat("Speed", 1);
        }
    }

    protected override Sprite GetHeadSprite()
    {
        return IsInspected() ? banker.GetComponent<DialogActor>().Avatar : null;
    }
}
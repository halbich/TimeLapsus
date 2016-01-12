using UnityEngine;

public class VaseController : InspectObjectController
{
    public VaseController()
    {
        canLoadHeadImage = true;
    }

    public string VaseFirstSeenDialog;
    public string StillCantTakeItemDialog;
    public string PotterDeadVarKey;

    public PotterDialogActor PotterDialogActor;

    private const string firstLineSeen = "vaseFirstLineSeen";

    protected override string getDialog()
    {
        // is potter dead?
        if (currentQuest.GetBoolean(PotterDeadVarKey))
            return base.getDialog();

        PotterDialogActor.SetFistFlag();

        //zobrazil jsem první linku?
        if (!currentQuest.GetBoolean(firstLineSeen))
            return VaseFirstSeenDialog;

        //zobraz ne
        return StillCantTakeItemDialog;
    }

    protected override void endDialogAction()
    {
        if (currentQuest.GetBoolean(PotterDeadVarKey))
            base.endDialogAction();

        if (!currentQuest.GetBoolean(firstLineSeen))
            currentQuest.SetBoolean(firstLineSeen);
    }

    protected override Sprite GetHeadSprite()
    {
        return PotterDialogActor != null ? PotterDialogActor.Avatar : null;
    }
}
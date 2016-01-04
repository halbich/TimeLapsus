using UnityEngine;
using System.Collections;

public class VaseController : InspectObjectController
{


    public string VaseFirstSeenDialog;
    public string StillCantTakeItemDialog;
    public string PotterDeadVarKey;

    private const string firstLineSeen = "vaseFirstLineSeen";


    protected override string getDialog()
    {

        // is potter dead?
        if (currentQuest.GetBoolean(PotterDeadVarKey))
            return base.getDialog();

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
            currentQuest.SetValue(firstLineSeen, true);
    }


}

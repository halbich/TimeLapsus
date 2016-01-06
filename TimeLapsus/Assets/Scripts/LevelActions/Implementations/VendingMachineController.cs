using UnityEngine;

public class VendingMachineController : InspectObjectController
{
    public string KnownAutomatName;
  
    public string AutomatEmptyDialog;

    public string ChipWasGivenVarName;


    public GameObject ChipObject;

    protected override void Start()
    {
        base.Start();
        if (IsInspected())
            GetComponent<InspectObject>().Name = KnownAutomatName;

        if (!currentQuest.GetBoolean(ChipWasGivenVarName) && ChipObject != null)
            ChipObject.SetActive(false);
    }

    protected override string getDialog()
    {
        return currentQuest.GetBoolean(ChipWasGivenVarName) ? AutomatEmptyDialog : base.getDialog();
    }

    protected override void endDialogAction()
    {
        base.endDialogAction();
        GetComponent<InspectObject>().Name = KnownAutomatName;
    }
}
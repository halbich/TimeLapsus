using UnityEngine;

public class VendingMachineController : InspectObjectController
{

    public string KnownAutomatName;
    public string HasChipVarName;
    public string HasChipDialog;
    public GameObject ChipObject;

   

    protected override void Start()
    {
        base.Start();
        if(IsInspected())
            GetComponent<InspectObject>().Name = KnownAutomatName;

         if (!currentQuest.GetBoolean(HasChipVarName) && ChipObject != null)
             ChipObject.SetActive(false);

    }

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(HasChipVarName))
            return HasChipDialog;

        return base.getDialog();
    }

    protected override void endDialogAction()
    {
        base.endDialogAction();
        GetComponent<InspectObject>().Name = KnownAutomatName;
    }
}
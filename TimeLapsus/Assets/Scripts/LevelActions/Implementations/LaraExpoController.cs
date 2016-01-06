using UnityEngine;

public class LaraExpoController : InspectObjectController
{
    public string VaseIsInExpoVarName;
    public string VaseInExpoDialog;

    public GameObject Vase;

    protected override void Start()
    {
        base.Start();
        UpdateItems();
    }

    public void UpdateItems()
    {
        Vase.SetActive(currentQuest.GetBoolean(VaseIsInExpoVarName));
    }

    protected override string getDialog()
    {
        return currentQuest.GetBoolean(VaseIsInExpoVarName) ? VaseInExpoDialog : base.getDialog();
    }
}
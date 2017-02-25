using UnityEngine;

public class GravePresentController : InspectObjectController
{
    public string AfterInsertedInfoDialog;
    public string AfterInsertedTitle;
    public string InsertedVaseVarName;

    // public UseVaseOnGravePastscript InsertScript;
    public GameObject VaseInGrave;

    public string AfterBuriedInfoDialog;
    public string AfterBuriedTitle;
    public string BuriedVaseVarName;

    //public DigVaseIntoGrave BuryScript;
    public GameObject PileOfDirtWithFlowers;

    public ShovelOutGrave ShovelOutGrave;

    public string GraveShoveledInfoDialog;
    public string GraveShoveledTitle;

    protected override void Start()
    {
        base.Start();
        ActionOccured();
    }

    internal void ActionOccured()
    {
        var hasInserted = currentQuest.GetBoolean(InsertedVaseVarName);

        if (hasInserted)
        {
            InspectedItemDialog = AfterInsertedInfoDialog;
            GetComponent<InspectObject>().Name = AfterInsertedTitle;
        }

        var hasBuried = currentQuest.GetBoolean(BuriedVaseVarName);

        PileOfDirtWithFlowers.SetActive(hasInserted && hasBuried);
        VaseInGrave.SetActive(hasInserted && hasBuried);
        ShovelOutGrave.enabled = hasInserted && hasBuried;

        if (hasBuried)
        {
            InspectedItemDialog = AfterBuriedInfoDialog;
            GetComponent<InspectObject>().Name = AfterBuriedTitle;
        }

        var Shoveled = currentQuest.GetBoolean(ShovelOutGrave.ShoveledGraveKeyName);
        if (Shoveled)
        {
            // Destroy(PileOfDirtWithFlowers);
            PileOfDirtWithFlowers.SetActive(false);
            InspectedItemDialog = GraveShoveledInfoDialog;
            GetComponent<InspectObject>().Name = GraveShoveledTitle;
        }
    }

    protected override void endDialogAction()
    {
        var Shoveled = currentQuest.GetBoolean(ShovelOutGrave.ShoveledGraveKeyName);
        if (Shoveled)
        {
            base.endDialogAction();
        }
    }
}
using Assets.Scripts.InventoryManagement;
using UnityEngine;

public class GravePastController : InspectObjectController
{

    public string AfterInsertedInfoDialog;
    public string AfterInsertedTitle;
    public UseVaseOnGravePastscript InsertScript;
    public GameObject VaseInGrave;

    public string AfterBuriedInfoDialog;
    public string AfterBuriedTitle;
    public DigVaseIntoGrave BuryScript;
    public GameObject PileOfDirt;



    public GameObject DeathObject;

    private Sprite headSprite;

    public GravePastController()
    {
        canLoadHeadImage = true;
    }

    protected override void Start()
    {
        base.Start();

         if (DeathObject == null)
            return;

        headSprite = DeathObject.GetComponent<DialogActor>().Avatar;


        ActionOccured();
    }

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(BuryScript.BuriedVaseVarName))
        {
            return AfterBuriedInfoDialog;
        }

        return base.getDialog();
    }


    protected override Sprite GetHeadSprite()
    {

        var hasInserted = currentQuest.GetBoolean(InsertScript.InsertedVaseVarName);

        var hasBuried = currentQuest.GetBoolean(BuryScript.BuriedVaseVarName);

        return hasInserted && !hasBuried ? headSprite : null;
    }

    internal void ActionOccured()
    {

        var hasInserted = currentQuest.GetBoolean(InsertScript.InsertedVaseVarName);

        InsertScript.enabled = !hasInserted;
        VaseInGrave.SetActive(hasInserted);

        if (hasInserted)
        {
            InspectedItemDialog = AfterInsertedInfoDialog;
            GetComponent<InspectObject>().Name = AfterInsertedTitle;
        }

        var hasBuried = currentQuest.GetBoolean(BuryScript.BuriedVaseVarName);

        BuryScript.enabled = hasInserted && !hasBuried;
        PileOfDirt.SetActive(hasBuried);

        if (hasBuried)
        {
            InspectedItemDialog = AfterBuriedInfoDialog;
            GetComponent<InspectObject>().Name = AfterBuriedTitle;
        }


    }
}
using UnityEngine;

public class LaraSpeakController : DialogActorController
{
    public string NoVaseDialog1;
    public string NoVaseDialog2;

    public string LaraHasVaseOnTableVarName;
    public GameObject VaseOnTable;

    public string LaraHasMoneyOnTable;
    public GameObject MoneyOnTable;

    public string LaraHasVaseDialog;
    public LaraExpoController LaraExpo;

    private const string HasSpoken = "hasSpokenWithLara";

    protected override void Start()
    {
        base.Start();
      UpdateItems();
    }

    protected override string getDialog()
    {
        if (currentQuest.GetBoolean(LaraExpo.VaseIsInExpoVarName))
            return LaraHasVaseDialog;

        return currentQuest.GetBoolean(HasSpoken) ? NoVaseDialog2 : NoVaseDialog1;
    }

    protected override void endDialogAction()
    {
        if(!currentQuest.GetBoolean(HasSpoken))
        currentQuest.SetBoolean(HasSpoken);
    }



    internal void UpdateItems()
    {
        VaseOnTable.SetActive(currentQuest.GetBoolean(LaraHasVaseOnTableVarName));
        MoneyOnTable.SetActive(currentQuest.GetBoolean(LaraHasMoneyOnTable));
    }
}
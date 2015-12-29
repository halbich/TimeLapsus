public class GravePastController : InspectObjectController
{
    private const string KeyName = "hasSeenGravePast";

    protected override string getDialog()
    {
        bool buried;
        if (currentQuest.TryGetValue("buriedVase", out buried) && buried)
        {
            return "inspectPastVaseBuried";
        }

        bool hasShovel;
        if (currentQuest.TryGetValue(KeyName, out hasShovel) && hasShovel)
        {
            return null;
        }
        return "inspectGravePast";
    }

    protected override void endDialogAction()
    {
    }
}
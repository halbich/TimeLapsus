public class GravePresentController : InspectObjectController
{

    private const string KeyName = "hasDiggedVase";

    protected override string getDialog()
    {

        bool buried;
        if (currentQuest.TryGetValue("buriedVase", out buried) && buried)
        {
            return "inspectPresentVaseBuried";
        }


        bool hasShovel;
        if (currentQuest.TryGetValue(KeyName, out hasShovel) && hasShovel)
        {
            return null;
        }
        return "inspectGravePresent";
    }

    protected override void endDialogAction()
    {
       
    }
}

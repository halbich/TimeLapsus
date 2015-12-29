public class ShovelController : InspectObjectController
{
    private const string KeyName = "hasShovel";

    protected override string getDialog()
    {


        bool hasShovel;
        if (currentQuest.TryGetValue(KeyName, out hasShovel) && hasShovel)
        {
            return "hasShovel";
        }
        return "inspectShovel";
    }

    protected override void endDialogAction()
    {
        currentQuest.SetValue(KeyName, true);
       Destroy(gameObject);
    }
}

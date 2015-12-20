public class VendingMachineController : InspectObjectController
{
    private const string KeyName = "hasShovel";

    protected override string getDialog()
    {
        var cQuest = QuestController.Instance.GetCurrent();


        //pokud mám čip, mluv

        bool hasShovel;
        if (cQuest.TryGetValue(KeyName, out hasShovel) && hasShovel)
        {
            return "hasShovel";
        }
        return "inspectShovel";
    }
}

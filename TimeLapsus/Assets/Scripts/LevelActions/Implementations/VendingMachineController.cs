public class VendingMachineController : InspectObjectController
{
    private const string KeyName = "hasMoneyToBuy";

    protected override string getDialog()
    {
        bool hasMoney;
        if (currentQuest.TryGetValue(KeyName, out hasMoney) && hasMoney)
        {
            return "hasMoneyToBuy";
        }
        return "needMoneyToBuy";
    }
}
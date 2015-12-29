public class VaseController : InspectObjectController
{
    private const string KeyName = "inspectedVase";

    private void Start()
    {
        bool inspectedVase;
        if (currentQuest.TryGetValue(KeyName, out inspectedVase) && inspectedVase)
        {
            enabled = false;
        }
    }

    protected override string getDialog()
    {
        return "inspectVase";
    }

    protected override void endDialogAction()
    {
        currentQuest.SetValue(KeyName, true);
        GetComponent<InspectObject>().enabled = false;
        GetComponent<PickableItem>().enabled = true;
    }
}
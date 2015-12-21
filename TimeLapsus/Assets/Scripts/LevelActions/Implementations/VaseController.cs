public class VaseController : InspectObjectController
{
    public VaseController()
    {
    }
    private const string KeyName = "inspectedVase";

    void Start()
    {
        bool inspectedVase;
        if (currentQuest.TryGetValue(KeyName, out inspectedVase) && inspectedVase)
        {
            this.enabled = false;
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

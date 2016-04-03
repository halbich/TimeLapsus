public class TeleportController : InspectObjectController
{
    public string KnownTeleportName;

    public string FirstSeenDialogName;
    public string SimilarSeenDialogName;
    public string HasSeenTeleportKeyName;

    protected override void Start()
    {
        base.Start();

        var hasSpoken = "hasTriggeredAfterTeleportDialog";

        if (currentQuest.GetBoolean(hasSpoken))
        {
            GetComponent<ChangeTimeLine>().Name = KnownTeleportName;
            GetComponent<InspectObject>().enabled = false;
            return;
        }

        if (!IsInspected())
        {
            GetComponent<ChangeTimeLine>().enabled = false;

        }
        else
        {
            if (currentQuest.GetBoolean(FirstSeenDialogName))
            {
                GetComponent<InspectObject>().enabled = false;
            }
            else
            {
                GetComponent<ChangeTimeLine>().enabled = false;
            }
        }
    }

    protected override string getDialog()
    {
        if (!currentQuest.GetBoolean(HasSeenTeleportKeyName))
            return FirstSeenDialogName;

        return SimilarSeenDialogName;
    }

    protected override void endDialogAction()
    {
        if (!currentQuest.GetBoolean(HasSeenTeleportKeyName))
        {
            currentQuest.SetBoolean(HasSeenTeleportKeyName);
            currentQuest.SetBoolean(FirstSeenDialogName);
        }

        base.endDialogAction();
        var timeline = GetComponent<ChangeTimeLine>();
        timeline.enabled = true;
        timeline.SetIsInBox(true);
        GetComponent<InspectObject>().enabled = false;
        Controller.CursorManager.SetCursor(timeline.GetCurrentCursor());
    }
}
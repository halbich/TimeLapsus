using UnityEngine;
using System.Collections;

public class TeleportController : InspectObjectController
{
    public string KnownTeleportName;
    private void Start()
    {
        if (!IsInspected())
            GetComponent<ChangeTimeLine>().enabled = false;

        var hasSpoken = "hasTriggeredAfterTeleportDialog";

        bool hasAlreadySpeaked;
        if (currentQuest.TryGetValue(hasSpoken, out hasAlreadySpeaked) && hasAlreadySpeaked)
        {
            GetComponent<ChangeTimeLine>().Name = KnownTeleportName;
        }

    }

    protected override void endDialogAction()
    {
        base.endDialogAction();
        GetComponent<ChangeTimeLine>().enabled = true;
        GetComponent<InspectObject>().enabled = false;
    }
}

using UnityEngine;
using System.Collections;

public class TeleportController : InspectObjectController
{
    private void Start()
    {
        if (!IsInspected())
            GetComponent<ChangeTimeLine>().enabled = false;
    }

    protected override void endDialogAction()
    {
        base.endDialogAction();
        GetComponent<ChangeTimeLine>().enabled = true;
        GetComponent<InspectObject>().enabled = false;
    }
}

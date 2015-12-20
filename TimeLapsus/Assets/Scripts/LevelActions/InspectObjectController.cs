using UnityEngine;

public abstract class InspectObjectController : MonoBehaviour
{

    public void Inspect()
    {
        var di = DialogController.Instance;
        var dialog = di.GetDialog(getDialog());

        if (di != null)
            di.ShowDialog(dialog, null, endDialogAction);

    }

    protected abstract string getDialog();

    protected virtual void endDialogAction()
    {

    }
}

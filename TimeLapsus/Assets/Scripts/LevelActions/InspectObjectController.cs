using UnityEngine;
using System.Collections;

public abstract class InspectObjectController : MonoBehaviour {

    public void Inspect()
    {
        var di = DialogController.Instance;
        var dialog = di.GetDialog(getDialog());
        di.ShowDialog(dialog, null, endDialogAction );

    }

    protected abstract string getDialog();

    protected virtual void endDialogAction()
    {
        
    }
}

using UnityEngine;

public abstract class InspectObjectController : MonoBehaviour
{

    public string InspectedItemVariable;
    public string InspectedItemDialog;

    protected Quest currentQuest = QuestController.Instance.GetCurrent();

    public void Inspect()
    {
        var di = DialogController.Instance;
        var dialog = di.GetDialog(getDialog());

        if (di != null)
            di.ShowDialog(dialog, null, endDialogAction);

        var pickable = GetComponent<PickableItem>();
        if (pickable != null)
            pickable.IsInspected();
    }

    protected virtual string getDialog()
    {
        return InspectedItemDialog;
    }

    protected virtual void endDialogAction()
    {
        Debug.LogFormat("Set true to variable {0}", InspectedItemVariable);
        currentQuest.SetBoolean(InspectedItemVariable);
    }

    public bool IsInspected()
    {
        return currentQuest.GetBoolean(InspectedItemVariable);
    }
}
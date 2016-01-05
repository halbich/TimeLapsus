using UnityEngine;

public abstract class InspectObjectController : ScriptWithController
{

    public string InspectedItemVariable;
    public string InspectedItemDialog;
    protected bool canLoadHeadImage;

    protected Quest currentQuest = QuestController.Instance.GetCurrent();

    public void Inspect()
    {
        var di = DialogController.Instance;
        var dialog = di.GetDialog(getDialog());

        if (di != null)
            di.ShowDialog(dialog, canLoadHeadImage ? null : GetHeadSprite(), endDialogAction);

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
        currentQuest.SetBoolean(InspectedItemVariable);
    }

    public bool IsInspected()
    {
        return currentQuest.GetBoolean(InspectedItemVariable);
    }

    protected virtual Sprite GetHeadSprite()
    {
        return null;
    }
}
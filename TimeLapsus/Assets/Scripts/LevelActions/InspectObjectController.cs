using UnityEngine;

public abstract class InspectObjectController : ScriptWithController
{
    // If true, the script will try to automatically pick up an attached item. And do nothing if no PickableItem component is attached.
    public bool AutoPickUp = false;
    public string InspectedItemVariable;
    public string InspectedItemDialog;
    protected bool canLoadHeadImage;

    protected Quest currentQuest = QuestController.Instance.GetCurrent();

    public void Inspect()
    {
        var di = DialogController.Instance;
        var dialog = di.GetDialog(getDialog());

        if (di != null)
            di.ShowDialog(dialog, canLoadHeadImage ? GetHeadSprite() : null, endDialogAction);

        var pickable = GetComponent<PickableItem>();
        if (pickable != null && !AutoPickUp)
            pickable.SetInspected();
    }

    protected virtual string getDialog()
    {
        return InspectedItemDialog;
    }

    protected virtual void endDialogAction()
    {
        if (!currentQuest.GetBoolean(InspectedItemVariable))
            currentQuest.SetBoolean(InspectedItemVariable);
        if (AutoPickUp)
        {
            var itemObject = GetComponent<PickableItem>();
            if (itemObject != null)
                itemObject.PickUp();
        }
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
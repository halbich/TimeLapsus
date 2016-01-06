public class PickableItem : InspectObject
{
    public string pickedUpItemVariable;
    public EnumItemID itemId;

    protected Quest currentQuest = QuestController.Instance.GetCurrent();

    protected override void OnMouseDown()
    {
        if (!InspectController.IsInspected())
        {
            base.OnMouseDown();
            return;
        }

        if (!enabled || IsOverUI())
            return;

        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                PickUp();
            });
        else
            PickUp();
    }

    protected override void Start()
    {
        base.Start();

        if (currentQuest.GetBoolean(pickedUpItemVariable))
        {
            Destroy(gameObject);
            return;
        }

        if (InspectController.IsInspected())
        {
            SetInspected();
        }
    }

    public void PickUp()
    {
        Controller.AddInventoryItem(itemId);

        if (pickedUpItemVariable != null)
            currentQuest.SetBoolean(pickedUpItemVariable);

        Destroy(gameObject);
    }

    internal virtual void SetInspected()
    {
        cursor = CursorType.PickUp;
    }
}
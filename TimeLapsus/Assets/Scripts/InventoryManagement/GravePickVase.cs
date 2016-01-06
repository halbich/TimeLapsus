public class GravePickVase : PickableItem
{
    public ShovelOutGrave ShovelScript;

    internal override void SetInspected()
    {
        if (currentQuest.GetBoolean(ShovelScript.ShoveledGraveKeyName))
            base.SetInspected();
    }
}
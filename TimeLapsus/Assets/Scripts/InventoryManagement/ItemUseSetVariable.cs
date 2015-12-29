public class ItemUseSetVariable : ItemUseOnScript
{
    public string VariableToSet;
    public string ValueToSet;

    public override void Use()
    {
        base.Use();
        QuestController.Instance.GetCurrent().SetValue(VariableToSet, ValueToSet);
    }
}
using System.Linq;

public class ItemUseOnScript : ScriptWithController
{
    public EnumItemID itemID;
    public bool RemoveOnUse;
    protected ItemPoint ObjectPoint;
    public string Name;

    public virtual void Use()
    {
        if (RemoveOnUse)
            Controller.RemoveInventoryItem(itemID);
    }

    protected override void Start()
    {
        base.Start();
        var comps = FindObjectsOfType<ItemPointScript>().SingleOrDefault(e => e.BelongsToObject == itemID);
        if (comps == null)
            return;
        
        ObjectPoint = comps.GetPoint(Controller.CharacterZPosition);
        Destroy(comps);
    }
}
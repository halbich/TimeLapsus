using UnityEngine;

public class ItemUseOnScript : ScriptWithController
{
    public EnumItemID itemID;
    public bool RemoveOnUse;
    protected DirectionPoint ObjectPoint;
    public string Name;

    public virtual void Use()
    {
        if (RemoveOnUse)
            Controller.RemoveInventoryItem(itemID);

        Debug.LogFormat("Item {0} used.", itemID);
    }

    protected override void Start()
    {
        base.Start();
        var comps = GetComponentInChildren<ItemPointScript>();
        if (comps == null)
            return;

        ObjectPoint = comps.GetPoint(Controller.CharacterZPosition);

        //we can have multiple itemUseOnScripts, so Destroy is definitelly not a good idea
        //  Destroy(comps);
    }
}
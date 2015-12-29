using UnityEngine;

namespace Assets.Scripts
{
    public class InventoryItem : Object
    {
        public InventoryItem()
        {
        }

        public InventoryItem(string ItemName, string ItemDescription, EnumItemID ItemID, string spritePath)
        {
            this.ItemName = ItemName;
            this.ItemDescription = ItemDescription;
            this.ItemID = ItemID;
            ItemSprite = Resources.Load<Sprite>(spritePath);
        }

        public string ItemName;
        public string ItemDescription;
        public EnumItemID ItemID;
        public Sprite ItemSprite;
    }
}
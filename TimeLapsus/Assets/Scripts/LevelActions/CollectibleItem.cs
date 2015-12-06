using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.LevelActions
{
    class CollectibleItem : ClickableArea
    {
        public CollectibleItem()
        {
        }
        public string ItemID;
        public string Description;
        public string ItemName;
        public string StateVariableName;
        void Start()
        {
            int value;
            if (Statics.GlobalVariables.TryGetValue(StateVariableName, out value) && value == 2)
                Destroy(gameObject);
            else
            {
                switch (value)
                {
                    case 0:
                        cursor = CursorType.Explore;
                        break;
                    case 1:
                        cursor = CursorType.PickUp;
                        break;
                    default:
                        cursor = CursorType.Main;
                        break;
                }
            }
        }
        void Examine()
        {
            Controller.DialogController.ShowMessage(Description);
        }
        void AddToInventory()
        {
            Statics.Inventory.Add(Statics.AllInventoryItems[ItemID]);
        }
        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            Controller.DisplayedDescription = ItemName;
        }
        protected override void OnMouseExit()
        {
            base.OnMouseExit();
            Controller.DisplayedDescription = "";
        }
        private void OnMouseDown()
        {
            if (IsInBox && !Controller.DialogueActive)
            {
                int value;
                if (!Statics.GlobalVariables.TryGetValue(StateVariableName, out value))
                {
                    value = 0;
                }
                switch (value)
                {
                    case 0:
                        Examine();
                        Statics.GlobalVariables[StateVariableName] = 1;
                        cursor = CursorType.PickUp;
                        break;
                    case 1:AddToInventory();
                        Statics.GlobalVariables[StateVariableName] = 2;
                        Destroy(gameObject);
                        break;
                }
            }
        }
    }
}

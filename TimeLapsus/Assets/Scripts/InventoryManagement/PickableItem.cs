using UnityEngine;
using System.Collections;
using System.Linq;

public class PickableItem : ScriptWithController
{
    public EnumObjectID EntityID;

    public string pickedUpItemVariable;
    public string examinedItemVariable;
    public EnumItemID itemId;
    protected Quest currentQuest = QuestController.Instance.GetCurrent();

    private PickablePoint ObjectPoint;

    private void OnMouseEnter()
    {
        if (!enabled) return;
        Controller.CursorManager.SetCursor(CursorType.PickUp);
    }

    private void OnMouseExit()
    {
        if (!enabled) return;
        Controller.CursorManager.SetCursor();
    }

    private void OnMouseDown()
    {
        if (!enabled) return;
        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                PickUp();
                //QuestController.Instance.Inspect(EntityID);
            });
        else
            PickUp();
    }

    protected override void Start()
    {
        base.Start();

        bool pickedUpItem;
        bool examined;
        if (currentQuest.TryGetValue(pickedUpItemVariable, out pickedUpItem) && pickedUpItem)
        {
            Destroy(gameObject);
            return;
        }
        if (!(currentQuest.TryGetValue(examinedItemVariable, out examined)) || !examined)
        {
            enabled = false;
        }

        var comps = FindObjectsOfType<PickablePointScript>().SingleOrDefault(e => e.BelongsToObject == EntityID);
        if (comps == null)
            Debug.LogErrorFormat("No object pickable point defined for {0}! ", gameObject.name);
        else
        {
            ObjectPoint = comps.GetPoint(Controller.CharacterZPosition);
            Destroy(comps);
        }
    }
    public void PickUp()
    {
        Controller.AddInventoryItem(itemId);
        if (pickedUpItemVariable != null) currentQuest.SetValue(pickedUpItemVariable, true);
        Destroy(gameObject);
    }
}

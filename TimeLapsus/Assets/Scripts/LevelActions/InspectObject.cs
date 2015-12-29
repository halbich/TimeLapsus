using System.Linq;
using UnityEngine;

public class InspectObject : ClickableArea
{
    public InspectObject()
    {
        cursor = CursorType.Explore;
    }
    public EnumObjectID EntityID;


    private InspectPoint ObjectPoint;

    private InspectObjectController inspectController;



    private void OnMouseDown()
    {
        if (!enabled || IsOverUI()) return;
        if (ObjectPoint != null)

            Controller.PlayerController.MoveTo(ObjectPoint.StartPoint, () =>
            {
                Controller.PlayerController.SetNewFacing(ObjectPoint.Direction);
                inspectController.Inspect();
                //QuestController.Instance.Inspect(EntityID);
            });
        else
            inspectController.Inspect();
    }

    protected override void Start()
    {
        base.Start();


        var comps = FindObjectsOfType<InspectPointScript>().SingleOrDefault(e => e.BelongsToObject == EntityID);
        if (comps == null)
            Debug.LogErrorFormat("No object inspect point defined for {0}! ", gameObject.name);
        else
        {
            ObjectPoint = comps.GetPoint(Controller.CharacterZPosition);
            Destroy(comps);
        }

        inspectController = GetComponent<InspectObjectController>();
        if (inspectController == null)
            Debug.LogErrorFormat("No InspectObjectController defined for {0}! ", gameObject.name);
    }
}

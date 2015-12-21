using UnityEngine;

public class PickablePointScript : MonoBehaviour
{

    public EnumObjectID BelongsToObject;

    public Facing Direction;

    public PickablePoint GetPoint(float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new PickablePoint(BelongsToObject, point, Direction);
    }



}

public class PickablePoint : DirectionPoint
{

    public readonly EnumObjectID BelongsToObject;

    public static bool operator ==(PickablePoint a, PickablePoint b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a.BelongsToObject == b.BelongsToObject && ArePointEqual(a, b);
    }

    public static bool operator !=(PickablePoint a, PickablePoint b)
    {
        return !(a == b);
    }

    public PickablePoint(EnumObjectID belongsToObject, Vector3 startPoint, Facing direction)
        : base(startPoint, direction)
    {
        BelongsToObject = belongsToObject;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
}


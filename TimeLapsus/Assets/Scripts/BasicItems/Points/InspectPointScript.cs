using UnityEngine;

public class InspectPointScript : MonoBehaviour
{
    public EnumObjectID BelongsToObject;

    public Facing Direction;

    public InspectPoint GetPoint(float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new InspectPoint(BelongsToObject, point, Direction);
    }
}

public class InspectPoint : DirectionPoint
{
    public readonly EnumObjectID BelongsToObject;

    public static bool operator ==(InspectPoint a, InspectPoint b)
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

    public static bool operator !=(InspectPoint a, InspectPoint b)
    {
        return !(a == b);
    }

    public InspectPoint(EnumObjectID belongsToObject, Vector3 startPoint, Facing direction)
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
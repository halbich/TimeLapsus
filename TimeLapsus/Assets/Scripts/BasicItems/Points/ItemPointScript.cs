using UnityEngine;

public class ItemPointScript : MonoBehaviour
{
    public EnumItemID BelongsToObject;

    public Facing Direction;

    public ItemPoint GetPoint(float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new ItemPoint(BelongsToObject, point, Direction);
    }
}

public class ItemPoint : DirectionPoint
{
    public readonly EnumItemID BelongsToObject;

    public static bool operator ==(ItemPoint a, ItemPoint b)
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

    public static bool operator !=(ItemPoint a, ItemPoint b)
    {
        return !(a == b);
    }

    public ItemPoint(EnumItemID belongsToObject, Vector3 startPoint, Facing direction)
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
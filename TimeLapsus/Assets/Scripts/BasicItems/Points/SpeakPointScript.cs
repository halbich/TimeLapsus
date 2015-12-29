using UnityEngine;

public class SpeakPointScript : MonoBehaviour
{
    public EnumActorID BelongsToActor;

    public Facing Direction;

    public SpeakPoint GetPoint(float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new SpeakPoint(BelongsToActor, point, Direction);
    }
}

public class SpeakPoint : DirectionPoint
{
    public readonly EnumActorID BelongsToActor;

    public static bool operator ==(SpeakPoint a, SpeakPoint b)
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
        return a.BelongsToActor == b.BelongsToActor && ArePointEqual(a, b);
    }

    public static bool operator !=(SpeakPoint a, SpeakPoint b)
    {
        return !(a == b);
    }

    public SpeakPoint(EnumActorID belongsToActor, Vector3 startPoint, Facing direction)
        : base(startPoint, direction)
    {
        BelongsToActor = belongsToActor;
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
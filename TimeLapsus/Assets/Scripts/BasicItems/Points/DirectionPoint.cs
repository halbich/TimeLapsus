using UnityEngine;

public class DirectionPoint
{
    public Vector3 StartPoint { get; private set; }

    public Facing Direction { get; private set; }

    public DirectionPoint(Vector3 startPoint, Facing direction)
    {
        StartPoint = startPoint;
        Direction = direction;
    }

    public static bool operator ==(DirectionPoint a, DirectionPoint b)
    {
        return ArePointEqual(a, b);
    }

    public static bool ArePointEqual(DirectionPoint a, DirectionPoint b)
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
        return a.StartPoint == b.StartPoint && a.Direction == b.Direction;
    }

    public static bool operator !=(DirectionPoint a, DirectionPoint b)
    {
        return !(a == b);
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
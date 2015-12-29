using UnityEngine;

public class RespawnPointScript : MonoBehaviour
{
    public EnumLevel LevelName;

    public Facing Direction;

    public RespawnPoint GetPoint(float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new RespawnPoint(LevelName, point, Direction);
    }
}

public class RespawnPoint : DirectionPoint
{
    public EnumLevel LevelName;

    public RespawnPoint(EnumLevel levelName, Vector3 startPoint, Facing direction)
        : base(startPoint, direction)
    {
        LevelName = levelName;
    }

    public static bool operator ==(RespawnPoint a, RespawnPoint b)
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
        return a.LevelName == b.LevelName && ArePointEqual(a, b);
    }

    public static bool operator !=(RespawnPoint a, RespawnPoint b)
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
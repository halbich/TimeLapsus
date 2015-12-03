using UnityEngine;
using System.Collections;

public class RespawnPointScript : MonoBehaviour
{

    public EnumLevel LevelName;

    public Facing Direction;

    public RespawnPoint GetPoint(GameObject obj, float CharacterZPosition)
    {
        var point = obj.transform.position;
        point.z = CharacterZPosition;
        return new RespawnPoint { LevelName = this.LevelName, Direction = this.Direction, StartPoint = point };
    }

}

public struct RespawnPoint
{

    public EnumLevel LevelName;

    public Vector3 StartPoint;

    public Facing Direction;

    public static bool operator ==(RespawnPoint a, RespawnPoint b)
    {
        // Return true if the fields match:
        return a.LevelName == b.LevelName && a.StartPoint == b.StartPoint && a.Direction == b.Direction;
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


using UnityEngine;
using System.Collections;

public class SpeakPointScript : MonoBehaviour
{

    public EnumActorID BelongsToActor;

    public Facing Direction;

    public SpeakPoint GetPoint( float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new SpeakPoint { BelongsToActor = this.BelongsToActor, Direction = this.Direction, StartPoint = point };
    }

    

}

public struct SpeakPoint
{
    public EnumActorID BelongsToActor;

    public Vector3 StartPoint;

    public Facing Direction;

    public static bool operator ==(SpeakPoint a, SpeakPoint b)
    {
        // Return true if the fields match:
        return a.BelongsToActor == b.BelongsToActor &&  a.StartPoint == b.StartPoint && a.Direction == b.Direction;
    }

    public static bool operator !=(SpeakPoint a, SpeakPoint b)
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


﻿using UnityEngine;

public class ItemPointScript : MonoBehaviour
{
    public Facing Direction;

    public DirectionPoint GetPoint(float CharacterZPosition)
    {
        var point = transform.position;
        point.z = CharacterZPosition;
        return new DirectionPoint(point, Direction);
    }
}
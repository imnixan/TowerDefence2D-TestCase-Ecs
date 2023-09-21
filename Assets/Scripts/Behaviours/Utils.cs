using System.Collections;
using UnityEngine;
using EpPathFinding.cs;
using Leopotam.Ecs;

public static class Utils
{
    public static Vector2 ConvertToWorld(this GridPos navPoint, Vector2 fieldSize)
    {
        return new Vector2(navPoint.x - fieldSize.x / 2, navPoint.y - fieldSize.y / 2);
    }

    public static GridPos ConvertToNav(this Vector2 worldPoint, Vector2 fieldSize)
    {
        return new GridPos(
            (int)worldPoint.x + (int)fieldSize.x / 2,
            (int)worldPoint.y + (int)fieldSize.y / 2
        );
    }

    public static GridPos ConvertToNav(this Vector3 worldPoint, Vector2 fieldSize)
    {
        return new GridPos(
            (int)worldPoint.x + (int)fieldSize.x / 2,
            (int)worldPoint.y + (int)fieldSize.y / 2
        );
    }
}

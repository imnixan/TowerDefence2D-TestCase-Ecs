using System.Collections;
using UnityEngine;
using EpPathFinding.cs;

public static class Utils
{
    public static Vector2 ConvertToWorld(this GridPos navPoint)
    {
        return new Vector2(navPoint.x - 50, navPoint.y - 50);
    }
}

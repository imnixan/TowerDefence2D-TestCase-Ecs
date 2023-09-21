using System.Collections;
using UnityEngine;
using EpPathFinding.cs;

public static class Utils
{
    public static Vector2 ConvertToWorld(this GridPos navPoint)
    {
        return new Vector2(navPoint.x - 50, navPoint.y - 50);
    }

    public static GridPos ConvertToNav(this Vector2 worldPoint)
    {
        return new GridPos((int)worldPoint.x + 50, (int)worldPoint.y + 50);
    }

    public static GridPos ConvertToNav(this Vector3 worldPoint)
    {
        return new GridPos((int)worldPoint.x + 50, (int)worldPoint.y + 50);
    }
}

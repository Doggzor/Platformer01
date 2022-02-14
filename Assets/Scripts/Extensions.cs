using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector2 RotatedByAngleZ(this Vector2 vec, float AngleZ)
    {
        float radiansRotationZ = -AngleZ * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(radiansRotationZ), Mathf.Cos(radiansRotationZ)) * vec.y +
        new Vector2(Mathf.Cos(radiansRotationZ), -Mathf.Sin(radiansRotationZ)) * vec.x;
    }
}

using UnityEngine;

public static class Extensions
{
    public static Vector2 RotatedByAngleZ(this Vector2 vec, float angleZ)
    {
        float radiansRotationZ = -angleZ * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(radiansRotationZ), Mathf.Cos(radiansRotationZ)) * vec.y +
        new Vector2(Mathf.Cos(radiansRotationZ), -Mathf.Sin(radiansRotationZ)) * vec.x;
    }

    public static void SetColorAlpha(this SpriteRenderer sr, float alphaValue)
    {
        Color modifiedColor = sr.color;
        modifiedColor.a = alphaValue;
        sr.color = modifiedColor;
    }
}

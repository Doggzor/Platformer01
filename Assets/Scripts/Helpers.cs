using UnityEngine;

public static class Helpers
{
    public static void DrawGizmosWireCube(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= transform;

        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldGizmosMatrix;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arcPosition">Center position of the arc</param>
    /// <param name="rotation">Rotation angle in degrees on Z axis</param>
    /// <param name="arcAngle">How big of a "pie piece" to draw</param>
    /// <param name="arcRadius">Radius of the arc</param>
    /// <param name="pointClockwise">In which direction to draw the arc and the arrow</param>
    public static void DrawGizmosRotationIndicator2D(Vector2 arcPosition, float rotation, float arcAngle, float arcRadius, bool pointClockwise = true)
    {
        Vector3 normal = pointClockwise ? Vector3.back : Vector3.forward;
        Vector2 from = Vector2.up.RotatedByAngleZ(rotation);
        UnityEditor.Handles.ArrowHandleCap(1, arcPosition + 1.1f * arcRadius * from, Quaternion.FromToRotation(normal, from.RotatedByAngleZ(90.0f)), 0.5f, EventType.Repaint);
        UnityEditor.Handles.DrawSolidArc(arcPosition, normal, from, arcAngle, arcRadius);
    }
}

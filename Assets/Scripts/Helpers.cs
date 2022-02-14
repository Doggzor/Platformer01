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
}

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dungeon.Door))]
public class DoorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var door = (Dungeon.Door)target;

        base.OnInspectorGUI();

        if (GUILayout.Button("Apply Color"))
        {
            door.ApplyColor();
        }
    }
}

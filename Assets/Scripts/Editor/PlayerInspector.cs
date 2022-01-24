using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var player = (Player)target;

        base.OnInspectorGUI();
        if (GUILayout.Button("Apply Skin"))
        {
            player.ApplySkin();
        }
    }
}

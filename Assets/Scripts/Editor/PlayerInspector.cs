using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dungeon.Player))]
public class PlayerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var player = (Dungeon.Player)target;

        base.OnInspectorGUI();
        player.ApplySkin();

    }
}

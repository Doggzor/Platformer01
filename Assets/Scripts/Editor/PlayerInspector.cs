using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dungeon.PlayerOld))]
public class PlayerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var player = (Dungeon.PlayerOld)target;

        base.OnInspectorGUI();
        player.ApplySkin();

    }
}

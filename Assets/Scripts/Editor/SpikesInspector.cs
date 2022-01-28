using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dungeon.Spikes))]
public class SpikesInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var spikes = (Dungeon.Spikes)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Rotate Left", EditorStyles.miniButtonMid))
        {
            spikes.transform.Rotate(new Vector3(0, 0, 90f));
        }
        if (GUILayout.Button("Flip"))
        {
            spikes.transform.Rotate(new Vector3(0, 0, 180f));
        }
        if (GUILayout.Button("Rotate Right"))
        {
            spikes.transform.Rotate(new Vector3(0, 0, -90f));
        }
        GUILayout.EndHorizontal();
    }
}

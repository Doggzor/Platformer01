using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(Spikes)), CanEditMultipleObjects]
    public class SpikesEditor : Editor
    {
        Spikes spikes;
        private void OnEnable()
        {
            spikes = (Spikes)target;
        }
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            Undo.RecordObject(spikes.transform, "Rotate Spikes");
            if (GUILayout.Button("Rotate Left"))
            {
                spikes.transform.Rotate(new Vector3(0, 0, 90f));
            }
            else if (GUILayout.Button("Flip"))
            {
                spikes.transform.Rotate(new Vector3(0, 0, 180f));
            }
            else if (GUILayout.Button("Rotate Right"))
            {
                spikes.transform.Rotate(new Vector3(0, 0, -90f));
            }
            PrefabUtility.RecordPrefabInstancePropertyModifications(spikes.transform);
            GUILayout.EndHorizontal();
        }
    }
}

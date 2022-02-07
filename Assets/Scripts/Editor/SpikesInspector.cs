using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(Spikes))]
    public class SpikesInspector : Editor
    {
        Spikes spikes;
        private void OnEnable()
        {
            spikes = (Spikes)target;
        }
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Rotate Left"))
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
}

using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(RotationSettings)), CanEditMultipleObjects]
    public class RotationSettingsEditor : Editor
    {
        RotationSettings t;
        private void OnEnable()
        {
            t = (RotationSettings)target;
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            Undo.RecordObject(t.transform, $"Rotate {t.name}");
            if (GUILayout.Button("Rotate Left"))
            {
                t.transform.Rotate(new Vector3(0, 0, 90f));
            }
            else if (GUILayout.Button("Flip"))
            {
                t.transform.Rotate(new Vector3(0, 0, 180f));
            }
            else if (GUILayout.Button("Rotate Right"))
            {
                t.transform.Rotate(new Vector3(0, 0, -90f));
            }
            PrefabUtility.RecordPrefabInstancePropertyModifications(t.transform);
            EditorGUILayout.EndHorizontal();
        }
    }
}

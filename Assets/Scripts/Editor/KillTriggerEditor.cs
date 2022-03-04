using UnityEngine;
using UnityEditor;

namespace Dungeon {
    [CustomEditor(typeof(KillTrigger))]
    public class KillTriggerEditor : Editor
    {
        private KillTrigger t;
        private Collider2D col;
        private void OnEnable()
        {
            t = (KillTrigger)target;
            col = t.GetComponent<Collider2D>();
        }
        public override void OnInspectorGUI()
        {
            if(!col.isTrigger)
            {
                EditorGUILayout.HelpBox("This script requires a 2D collider with 'Is Trigger' set to true in order to function.", MessageType.Warning);
            }
        }
    }
}

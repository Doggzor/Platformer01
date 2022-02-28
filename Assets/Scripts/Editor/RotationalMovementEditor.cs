using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(RotationalMovement)), CanEditMultipleObjects]
    public class RotationalMovementEditor : Editor
    {
        RotationalMovement t;
        private void OnEnable()
        {
            t = (RotationalMovement)target;
        }
        public override void OnInspectorGUI()
        {

            Undo.RecordObject(t, $"{t.name} Change");
            EditorGUI.BeginChangeCheck();

            t.Delay = EditorGUILayout.FloatField(new GUIContent("Delay", "Delay in seconds before the object starts moving"), Mathf.Max(0f, t.Delay));
            t.Speed = EditorGUILayout.FloatField(new GUIContent("Speed", "Angles per second"), Mathf.Max(0f, t.Speed));
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Direction");
            if (GUILayout.Button(t.IsMovingClockwise ? "Clockwise" : "Counter Clockwise"))
            {
                t.IsMovingClockwise = !t.IsMovingClockwise;
            }
            EditorGUILayout.EndHorizontal();
            t.MovementMode = (RotationalMovement.Mode)EditorGUILayout.EnumPopup("Mode", t.MovementMode);
            if (t.MovementMode != RotationalMovement.Mode.None)
            {
                t.Arc = EditorGUILayout.IntSlider("Arc", t.Arc, 0, 360);
                if (t.Arc == 0) EditorGUILayout.HelpBox("Object will not move if the arc is set to 0", MessageType.Warning);               
            }
            switch (t.MovementMode)
            {
                case RotationalMovement.Mode.SingleDirection:
                    EditorGUILayout.BeginHorizontal();
                    t.IsInfinite = EditorGUILayout.Toggle(new GUIContent("Infinite", "Should the movement pattern repeat indefinitely"), t.IsInfinite);
                    EditorGUI.BeginDisabledGroup(t.IsInfinite);
                    t.Cycles = EditorGUILayout.IntField(new GUIContent("Cycles", "How many times will the movement pattern repeat"), Mathf.Max(0, t.Cycles));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    t.HasDelayAtStartPos = EditorGUILayout.Toggle(new GUIContent("Pause", "Should the object pause after each cycle"), t.HasDelayAtStartPos);
                    EditorGUI.BeginDisabledGroup(!t.HasDelayAtStartPos);
                    t.DelayAtStartPos = EditorGUILayout.FloatField("Seconds", Mathf.Max(0f, t.DelayAtStartPos));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    break;
                case RotationalMovement.Mode.BackAndForth:
                    EditorGUILayout.BeginHorizontal();
                    t.IsInfinite = EditorGUILayout.Toggle(new GUIContent("Infinite", "Should the movement pattern repeat indefinitely"), t.IsInfinite);
                    EditorGUI.BeginDisabledGroup(t.IsInfinite);
                    t.Cycles = EditorGUILayout.IntField(new GUIContent("Cycles", "How many times will the movement pattern repeat"), Mathf.Max(0, t.Cycles));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    t.HasConstantSpeed = EditorGUILayout.Toggle(new GUIContent("Constant Speed", "Is speed the same in both directions\nNOTE: Overrides original speed if unchecked"), t.HasConstantSpeed);
                    if (!t.HasConstantSpeed)
                    {
                        t.OnwardSpeed = EditorGUILayout.FloatField(new GUIContent("Onward Speed", "Speed from the start to the end position"), Mathf.Max(0f, t.OnwardSpeed));
                        t.ReturnSpeed = EditorGUILayout.FloatField(new GUIContent("Return Speed", "Speed from the end to the start position"), Mathf.Max(0f, t.ReturnSpeed));
                    }
                    //Delay at start
                    EditorGUILayout.BeginHorizontal();
                    t.HasDelayAtStartPos = EditorGUILayout.Toggle(new GUIContent("Pause at Start", "Should the object wait after reaching the start position\nNOTE: This won't affect the first time the object moves, use \"Delay\" for that"), t.HasDelayAtStartPos);
                    EditorGUI.BeginDisabledGroup(!t.HasDelayAtStartPos);
                    t.DelayAtStartPos = EditorGUILayout.FloatField("Seconds", Mathf.Max(0f, t.DelayAtStartPos));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    //Delay at end
                    EditorGUILayout.BeginHorizontal();
                    t.HasDelayAtEndPos = EditorGUILayout.Toggle(new GUIContent("Pause at End", "Should the object wait after reaching the end position"), t.HasDelayAtEndPos);
                    EditorGUI.BeginDisabledGroup(!t.HasDelayAtEndPos);
                    t.DelayAtEndPos = EditorGUILayout.FloatField("Seconds", Mathf.Max(0f, t.DelayAtEndPos));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    break;
                default:
                    break;
            }

            PrefabUtility.RecordPrefabInstancePropertyModifications(t);

            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }
    }
}

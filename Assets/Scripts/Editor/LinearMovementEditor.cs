using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(LinearMovement)), CanEditMultipleObjects]
    public class LinearMovementEditor : Editor
    {
        LinearMovement t;
        private int selectedDir;
        private string[] buttonStrings = { "", "", "", "", "", "", "", "", "" };
        private void OnEnable()
        {
            t = (LinearMovement)target;
        }
        public override void OnInspectorGUI()
        {
            Undo.RecordObject(t, $"{t.name} Change");

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginHorizontal();

            //Left column
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight * 1.5f);
            t.Delay = EditorGUILayout.FloatField(new GUIContent("Delay", "Delay in seconds before the object starts moving"), Mathf.Max(0f, t.Delay));
            EditorGUILayout.Separator();
            t.Speed = EditorGUILayout.FloatField("Speed", Mathf.Max(0f, t.Speed));
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Relative to");
            if (GUILayout.Button(t.RelativeSpace.ToString()))
            {
                t.RelativeSpace = (Space)(((int)t.RelativeSpace + 1) % 2);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            //Right column
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PrefixLabel("Direction");
            EditorGUI.BeginChangeCheck();
            selectedDir = GUILayout.SelectionGrid(DirectionToIndex(), buttonStrings, 3, GUILayout.Height(120), GUILayout.MaxWidth(120));
            if (EditorGUI.EndChangeCheck())
            {               
                SetDirection();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            //Next Section
            EditorGUILayout.Separator();
            t.MovementMode = (LinearMovement.Mode)EditorGUILayout.EnumPopup("Mode", t.MovementMode);
            switch (t.MovementMode)
            {
                case LinearMovement.Mode.Once:
                    t.Distance = EditorGUILayout.FloatField("Distance", Mathf.Max(0.1f, t.Distance));
                    break;
                case LinearMovement.Mode.Patrol:
                    t.Distance = EditorGUILayout.FloatField("Distance", Mathf.Max(0.1f, t.Distance));
                    //Cycles
                    EditorGUILayout.BeginHorizontal();
                    t.IsInfinite = EditorGUILayout.Toggle(new GUIContent("Infinite", "Should the movement pattern repeat indefinitely"), t.IsInfinite);
                    EditorGUI.BeginDisabledGroup(t.IsInfinite);
                    t.Cycles = EditorGUILayout.IntField(new GUIContent("Cycles", "How many times will the movement pattern repeat"), Mathf.Max(0, t.Cycles));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    //Set custom speed
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

            PrefabUtility.RecordPrefabInstancePropertyModifications(t); //From testing, this doesn't seem to be needed, but whatever

            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
            
        }

        private void SetDirection()
        {
            t.Direction = Vector2.right * ((selectedDir % 3) - 1) + Vector2.down * ((selectedDir / 3) - 1);
        }
        private int DirectionToIndex()
        {
            return ((int)-t.Direction.y + 1) * 3 + ((int)t.Direction.x + 1);
        }
    }
}

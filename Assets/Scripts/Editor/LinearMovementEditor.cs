using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LinearMovement))]
    public class LinearMovementEditor : Editor
    {
        LinearMovement t;
        private int selectedDir;
        private string[] buttonStrings = { "", "", "", "", "", "", "", "", "" };
        private void OnEnable()
        {
            t = (LinearMovement)target;
            selectedDir = DirectionToIndex();
        }
        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();

            //Left column
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight * 1.5f);
            t.Delay = EditorGUILayout.FloatField(new GUIContent("Delay (Seconds)", "Delay in seconds before the object starts moving"), Mathf.Max(0f, t.Delay));
            EditorGUILayout.Separator();
            t.Speed = EditorGUILayout.FloatField("Speed", Mathf.Max(0f, t.Speed));
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Relative to");
            if (GUILayout.Button(t.RelativeSpace.ToString()))
            {
                Undo.RecordObject(t, "Change Relative Space");
                t.RelativeSpace = (Space)(((int)t.RelativeSpace + 1) % 2);
                PrefabUtility.RecordPrefabInstancePropertyModifications(t);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            //Right column
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PrefixLabel("Direction");
            EditorGUI.BeginChangeCheck();
            selectedDir = GUILayout.SelectionGrid(selectedDir, buttonStrings, 3, GUILayout.Height(120), GUILayout.MaxWidth(120));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.undoRedoPerformed += () => selectedDir = DirectionToIndex();
                Undo.RecordObject(t, "Change Direction");
                SetDirection();
                PrefabUtility.RecordPrefabInstancePropertyModifications(t);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            //Next Section
            EditorGUILayout.Separator();
            t.MovementMode = (LinearMovement.Mode)EditorGUILayout.EnumPopup("Mode", t.MovementMode);
            switch (t.MovementMode)
            {
                case LinearMovement.Mode.Continuous:
                    break;
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
                    //Delay Between Cycles
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    t.HasDelayBetweenCycles = EditorGUILayout.Toggle(new GUIContent("Delay (Cycles)", "How long to wait before moving after each full cycle"), t.HasDelayBetweenCycles);
                    if (EditorGUI.EndChangeCheck() && t.HasDelayBetweenCycles && t.HasDelayBetweenPositions) t.HasDelayBetweenPositions = false;
                    EditorGUI.BeginDisabledGroup(!t.HasDelayBetweenCycles);
                    t.DelayBetweenCycles = EditorGUILayout.FloatField("Seconds", Mathf.Max(0f, t.DelayBetweenCycles));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    //Delay Between Steps
                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginChangeCheck();
                    t.HasDelayBetweenPositions = EditorGUILayout.Toggle(new GUIContent("Delay (Steps)", "How long to wait before moving after each step"), t.HasDelayBetweenPositions);
                    if (EditorGUI.EndChangeCheck() && t.HasDelayBetweenCycles && t.HasDelayBetweenPositions) t.HasDelayBetweenCycles = false;
                    EditorGUI.BeginDisabledGroup(!t.HasDelayBetweenPositions);
                    t.DelayBetweenPositions = EditorGUILayout.FloatField("Seconds", Mathf.Max(0f, t.DelayBetweenPositions));
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.EndHorizontal();
                    break;
            }
        }

        private void SetDirection()
        {
            t.Direction = new Vector2( (selectedDir % 3) - 1, -((selectedDir / 3) - 1) );
        }
        private int DirectionToIndex()
        {
            return ((int)-t.Direction.y + 1) * 3 + ((int)t.Direction.x + 1);
        }
    }
}

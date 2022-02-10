using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(LinearMovement))]
    public class LinearMovementInspector : Editor
    {
        LinearMovement t;
        private int selectedDir;
        private string[] buttonStrings = { "", "", "", "", "", "", "", "", "" };
        private string currentRelativeButtonText;
        private void OnEnable()
        {
            t = (LinearMovement)target;
            selectedDir = DirectionToIndex();
            currentRelativeButtonText = t.relativeTo.ToString();
        }
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            //Left column
            GUILayout.BeginVertical();
            GUILayout.Space(EditorGUIUtility.singleLineHeight * 1.5f);
            t.delay = EditorGUILayout.FloatField(new GUIContent("Delay (Seconds)", "Delay in seconds before the object starts moving"), t.delay);
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            t.speed = EditorGUILayout.FloatField("Speed", t.speed);
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Relative to");
            if (GUILayout.Button(currentRelativeButtonText))
            {
                t.relativeTo = (LinearMovement.RelativeSpace)(((int)t.relativeTo + 1) % 2);
                currentRelativeButtonText = t.relativeTo.ToString();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            //Right column
            GUILayout.BeginVertical();
            EditorGUILayout.PrefixLabel("Direction");
            selectedDir = GUILayout.SelectionGrid(selectedDir, buttonStrings, 3, GUILayout.Height(120), GUILayout.MaxWidth(120));
            GUILayout.EndVertical();
            
            GUILayout.EndHorizontal();

            SetDirection();
        }

        private void SetDirection()
        {
            t.direction = new Vector2( (selectedDir % 3) - 1, -((selectedDir / 3) - 1) );
        }
        private int DirectionToIndex()
        {
            return ((int)-t.direction.y + 1) * 3 + ((int)t.direction.x + 1);
        }
    }
}

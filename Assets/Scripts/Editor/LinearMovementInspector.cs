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
        private int selectedDir = 4;
        private string[] buttonStrings = { "", "", "", "", "", "", "", "", "" };
        private void OnEnable()
        {
            t = (LinearMovement)target;
        }
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            base.OnInspectorGUI();
            GUILayout.EndVertical();

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
    }
}

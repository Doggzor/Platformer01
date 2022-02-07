/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomPropertyDrawer(typeof(LinearMovement))]
    public class LinearMovementPropertyDrawer : PropertyDrawer
    {
        SerializedProperty directionProperty;
        int selectedDir;
        Rect currentRect;
        float currentHeight;
        SerializedProperty foldoutExpanded;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            currentRect = new Rect(position.x, position.y + 8f, position.width, 16f);
            currentHeight = currentRect.height;

            directionProperty = property.FindPropertyRelative("direction");
            foldoutExpanded = property.FindPropertyRelative("FoldoutExpanded");

            EditorGUI.BeginProperty(Rect.zero, GUIContent.none, property);
            Rect r = new Rect(currentRect.x + 2, currentRect.y + 2, 16, 16);
            EditorGUI.DrawRect(r, Color.green);
            SpaceVertical();
            foldoutExpanded.boolValue = EditorGUI.Foldout(currentRect, foldoutExpanded.boolValue, "Movement", true);
            if (foldoutExpanded.boolValue)
            {
                SpaceVertical();
                EditorGUI.indentLevel++;
                EditorGUI.Vector2IntField(currentRect, directionProperty.displayName, directionProperty.vector2IntValue);
                SpaceVertical();
                selectedDir = EditorGUI.IntSlider(currentRect, "Selection Int", selectedDir, 0, 8);
                SpaceVertical();
                selectedDir = EditorGUI.IntSlider(currentRect, "Selection Int", selectedDir, 0, 8);
                SetDirection(selectedDir);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndProperty();
            
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + currentHeight - 8f;
        }
        private void SetDirection(int selectedDir)
        {
            int x = (selectedDir % 3) - 1;
            int y = -((selectedDir / 3) - 1);
            directionProperty.vector2IntValue = new Vector2Int(x, y);
        }
        private void SpaceVertical(float px = 20f)
        {
            currentHeight += px;
            currentRect.Set(currentRect.x, currentRect.y + px, currentRect.width, currentRect.height);
        }
    }
}
*/
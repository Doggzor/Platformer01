using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Dungeon
{
    [CustomEditor(typeof(ColorPicker)), CanEditMultipleObjects]
    public class ColorPickerEditor : Editor
    {
        private ColorPicker t;
        private SpriteRenderer renderer;
        private readonly Color[] colors = new Color[] {
            Color.red,
            new Color(1.0f, 0.5f, 0.0f),
            Color.yellow,
            Color.green,
            Color.cyan,
            Color.blue,
            new Color(0.5f, 0.0f, 1.0f),
            Color.magenta
        };
        private void OnEnable()
        {
            t = (ColorPicker)target;
            renderer = t.GetComponent<SpriteRenderer>();
        }
        public override void OnInspectorGUI()
        {
            Undo.RecordObject(renderer, $"Change {t.name} color");
            EditorGUILayout.BeginHorizontal();
            foreach (Color color in colors)
            {
                GUI.backgroundColor = color;
                if (GUILayout.Button(""))
                {
                    renderer.color = color;
                }
            }
            EditorGUILayout.EndHorizontal();
            PrefabUtility.RecordPrefabInstancePropertyModifications(renderer);
        }
    }
}

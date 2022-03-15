using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace Dungeon
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        private Door door;
        private Tilemap doorTiles;
        private SpriteRenderer keyRenderer;
        private Light keyGlow;
        private GUIStyle style = new GUIStyle();
        private readonly Color[] colors = new Color[] {
            Color.red,
            new Color(1.0f, 0.5f, 0.0f),
            Color.yellow,
            Color.green,
            Color.cyan,
            new Color(0.0f, 0.5f, 1.0f),
            new Color(0.5f, 0.0f, 1.0f),
            new Color(1.0f, 0.5f, 1.0f)
        };
        private void OnEnable()
        {
            door = (Door)target;
            doorTiles = door.GetComponent<Tilemap>();
            var keySprite = door.transform.Find("Key").Find("Sprite");
            keyRenderer = keySprite.GetComponent<SpriteRenderer>();
            keyGlow = keySprite.GetComponent<Light>();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 16;
            style.normal.textColor = Color.white;
        }
        public override void OnInspectorGUI()
        {
            Undo.RecordObject(doorTiles, $"Change {door.name} color");
            EditorGUILayout.LabelField("Color", style);
            EditorGUILayout.BeginHorizontal();
            foreach (Color color in colors)
            {
                GUI.backgroundColor = color;
                if (GUILayout.Button(""))
                {
                    doorTiles.color = color;
                }
            }
            EditorGUILayout.EndHorizontal();
            keyRenderer.color = keyGlow.color = doorTiles.color;
            PrefabUtility.RecordPrefabInstancePropertyModifications(doorTiles);
        }
    }
}

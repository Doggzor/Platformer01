using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

namespace Dungeon
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        private Door door;
        private Key key;
        private void OnEnable()
        {
            door = (Door)target;
            key = door.transform.Find("Key").GetComponent<Key>();
        }
        public override void OnInspectorGUI()
        {
            key.GetComponent<SpriteRenderer>().color = door.GetComponent<Tilemap>().color;
        }
    }
}

using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(Dungeon.Door))]
public class DoorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var door = (Dungeon.Door)target;
        var key = door.transform.Find("Key");
        key.GetComponent<SpriteRenderer>().color = door.GetComponent<Tilemap>().color;
    }
}

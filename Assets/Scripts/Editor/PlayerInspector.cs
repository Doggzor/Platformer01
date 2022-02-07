using UnityEngine;
using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(Player))]
    public class PlayerInspector : Editor
    {
        private Player player;

        private void OnEnable()
        {
            player = (Player)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            player.ApplySkin();
        }
    }
}

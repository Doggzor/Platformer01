using UnityEngine;

namespace Dungeon
{
    [System.Serializable]
    public struct PlayerControls
    {
        public PlayerControls(KeyCode left, KeyCode right, KeyCode jump)
        {
            leftKey = left;
            rightKey = right;
            jumpKey = jump;
        }
        public KeyCode leftKey;
        public KeyCode rightKey;
        public KeyCode jumpKey;
    }
}

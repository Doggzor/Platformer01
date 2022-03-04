using UnityEngine;

namespace Dungeon
{
    public abstract class PlayerInputSO : ScriptableObject
    {
        public float directionX { get; protected set; } = 0f;
        public float jumpPressTime { get; set; } = -1f;
        public float jumpReleaseTime { get; protected set; } = -1f;
        protected bool isEnabled = true;

        public abstract void Read();

        private void OnEnable()
        {
            ResetValues();
        }
        public virtual void Enable()
        {
            isEnabled = true;
        }
        public virtual void Disable()
        {
            isEnabled = false;
        }
        protected void ResetValues()
        {
            directionX = 0f;
            jumpPressTime = -1f;
            jumpReleaseTime = -1;
        }
    }
}
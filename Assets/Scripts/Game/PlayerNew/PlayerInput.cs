using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class PlayerInput
    {
        public float directionX { get; protected set; } = 0f;
        public float jumpPressTime { get; set; } = -1f;
        public float jumpReleaseTime { get; protected set; } = -1f;
        protected bool isEnabled = true;

        public abstract void Read();

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
        }
    }
}
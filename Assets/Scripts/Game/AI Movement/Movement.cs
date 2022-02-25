using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class Movement : MonoBehaviour
    {
        [Min(0f), Tooltip("Delay in seconds before the object starts moving")]
        public float Delay = 0f;
        [Min(0f), Tooltip("How fast the object moves")]
        public float Speed = 1;

        protected bool startMovement = false;
        protected virtual void Start()
        {
            Initialize();
            StartCoroutine(Co_WaitForDelay());
        }
        protected virtual void Update()
        {
            if (startMovement)
                UpdatePosition();
        }
        public virtual IEnumerator Co_WaitForDelay()
        {
            yield return new WaitForSeconds(Delay);
            startMovement = true;
        }
        public abstract void Initialize();
        public abstract void UpdatePosition();
    }
}

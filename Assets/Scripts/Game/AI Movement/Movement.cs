using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class Movement : MonoBehaviour
    {
        public float Delay = 0f;
        public float Speed = 1;

        protected bool startMovement = false;
        private void Start()
        {
            Initialize();
            StartCoroutine(Co_WaitForDelay());
        }
        private void Update()
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

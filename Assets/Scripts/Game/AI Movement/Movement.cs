using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class Movement : MonoBehaviour
    {
        [Tooltip("Delay in seconds before the object starts moving")]
        public float delay = 0f;
        [SerializeReference]
        public float speed = 1;

        protected bool startMovement = false;
        private void Start()
        {
            StartCoroutine(Co_WaitForDelay());
        }
        private void Update()
        {
            if (startMovement)
                UpdatePosition();
        }
        public virtual IEnumerator Co_WaitForDelay()
        {
            yield return new WaitForSeconds(delay);
            startMovement = true;
        }
        public abstract void UpdatePosition();
    }
}

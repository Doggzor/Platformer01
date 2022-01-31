using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class MovingBehaviour : MonoBehaviour
    {
        protected virtual void Update()
        {
            Move();
        }
        public abstract void Move();
    }
}

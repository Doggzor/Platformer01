using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class LinearMovement : Movement
    {
        public enum RelativeSpace
        {
            World,
            Self
        }
        [HideInInspector]
        public RelativeSpace relativeTo = RelativeSpace.World;
        [HideInInspector]
        public Vector2 direction = Vector2.zero;
        private Space space => relativeTo switch { RelativeSpace.World => Space.World, _ => Space.Self };

        public override void UpdatePosition()
        {
            transform.Translate(translation: speed * Time.deltaTime * direction, relativeTo: space);
        }
    }
}

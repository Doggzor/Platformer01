using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class LinearMovement : MovingBehaviour
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        [Range(-1, 1)]
        private int horizontalAxis;
        [SerializeField]
        [Range(-1, 1)]
        private int verticalAxis;
        [SerializeField]
        private bool relativeToSelf;

        private Vector3 direction;
        private Space space;

        private void Awake()
        {
            direction = new Vector3(horizontalAxis, verticalAxis, 0);
            space = relativeToSelf ? Space.Self : Space.World;
        }

        public override void Move()
        {
            transform.Translate(direction * speed * Time.deltaTime, space);
        }
    }
}

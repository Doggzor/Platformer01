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
        private bool relativeToWorldSpace = true;

        private Vector3 direction;
        private Space space;

        private void Awake()
        {
            direction = new Vector3(horizontalAxis, verticalAxis, 0);
            space = relativeToWorldSpace ? Space.World : Space.Self;
        }

        public override void Move()
        {
            transform.Translate(direction * speed * Time.deltaTime, space);
        }
    }
}

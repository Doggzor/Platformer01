using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class LinearMovement : Movement
    {
        public enum Mode
        {
            Once,
            Continuous,
            Patrol
        }
        public Vector2 Direction = Vector2.zero;
        public Space RelativeSpace = Space.World;
        public Mode MovementMode = Mode.Continuous;
        public float Distance = 0f;
        public bool IsInfinite = true;
        public int Cycles = 0;
        public bool HasDelayBetweenPositions = false;
        public bool HasDelayBetweenCycles = false;
        public float DelayBetweenPositions = 0f;
        public float DelayBetweenCycles = 0f;

        private Vector2 StartPos = Vector2.zero;
        private Vector2 EndPos = Vector2.zero;
        private Vector2 NextPos = Vector2.zero;
        private float currentDelayBetweenPositions = 0f;
        private float currentDelayBetweenCycles = 0f;
        public override void Initialize()
        {
            currentDelayBetweenPositions = DelayBetweenPositions;
            currentDelayBetweenCycles = DelayBetweenCycles;
            StartPos = transform.position;
            Direction = RelativeSpace == Space.World ? Direction : Direction.RotatedByAngleZ(transform.rotation.eulerAngles.z);
            EndPos = StartPos + Direction.normalized * Distance;
            NextPos = EndPos;
        }

        public override void UpdatePosition()
        {
            switch (MovementMode)
            {
                case Mode.Once:
                    transform.position = Vector2.MoveTowards(current: transform.position, target: NextPos, Speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, EndPos) < 0.01f)
                    {
                        Speed = 0f;
                    }
                    break;
                case Mode.Continuous:
                    transform.Translate(Speed * Time.deltaTime * Direction, RelativeSpace);
                    break;
                case Mode.Patrol:
                    transform.position = Vector2.MoveTowards(current: transform.position, target: NextPos, Speed * Time.deltaTime);
                    if (Cycles > 0 || IsInfinite)
                    {
                        if (Vector2.Distance(transform.position, NextPos) < 0.01f)
                        {
                            TryChangeNextPos();
                        }
                    }
                    else
                    {
                        Speed = 0f;
                    }
                    break;
            }
        }

        private void TryChangeNextPos()
        {
            if (HasDelayBetweenPositions && currentDelayBetweenPositions > 0f)
            {
                currentDelayBetweenPositions -= Time.deltaTime;
            }
            else
            {
                currentDelayBetweenPositions = DelayBetweenPositions;
                DetectNextPosition();
            }
        }

        private void DetectNextPosition()
        {
            if (NextPos == EndPos)
            {
                SetNextPosition(StartPos);
            }
            else
            {
                if (HasDelayBetweenCycles && currentDelayBetweenCycles > 0f)
                {
                    currentDelayBetweenCycles -= Time.deltaTime;
                }
                else
                {
                    SetNextPosition(EndPos);
                }
            }
        }

        private void SetNextPosition(Vector2 position)
        {
            NextPos = position;
            if (position == EndPos)
            {
                currentDelayBetweenCycles = DelayBetweenCycles;
                if (!IsInfinite) Cycles--;
            }
        }

        public void OnDrawGizmosSelected()
        {
            Color gizmoColor = Color.green;
            gizmoColor.a = 0.25f;
            Gizmos.color = gizmoColor;
            Vector2 direction = RelativeSpace == Space.World ? Direction : Direction.RotatedByAngleZ(transform.rotation.eulerAngles.z);

            if (MovementMode == Mode.Continuous)
            {
                Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction.normalized);
            }
            else
            {                
                Vector2 endPos = (Vector2)transform.position + direction.normalized * Distance;
                Gizmos.DrawLine(transform.position, endPos);
                Helpers.DrawGizmosWireCube(endPos, transform.rotation, transform.localScale);
            }
        }
    }
}

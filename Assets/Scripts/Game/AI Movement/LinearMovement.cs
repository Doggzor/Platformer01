using UnityEngine;

namespace Dungeon
{
    public class LinearMovement : Movement
    {
        public enum Mode
        {
            None,
            Once,
            Continuous,
            Patrol
        }
        public Vector2 Direction = Vector2.zero;
        public Space RelativeSpace = Space.World;
        public Mode MovementMode = Mode.Continuous;
        public float Distance = 0f;

        #region Patrol Specific Variables
        public bool HasConstantSpeed = true;
        public float OnwardSpeed = 0f;
        public float ReturnSpeed = 0f;
        public bool IsInfinite = true;
        public int Cycles = 0;
        public bool HasDelayAtEndPos = false;
        public bool HasDelayAtStartPos = false;
        public float DelayAtEndPos = 0f;
        public float DelayAtStartPos = 0f;
        private float currentDelayAtEndPos = 0f;
        private float currentDelayAtStartPos = 0f;
        private bool isTryingToChangePosition = false;
        #endregion

        private Vector2 StartPos = Vector2.zero;
        private Vector2 EndPos = Vector2.zero;
        private Vector2 NextPos = Vector2.zero;
        public override void Initialize()
        {
            currentDelayAtEndPos = DelayAtEndPos;
            currentDelayAtStartPos = DelayAtStartPos;
            StartPos = transform.position;
            Vector2 newDirection = RelativeSpace == Space.World ? Direction : Direction.RotatedByAngleZ(transform.rotation.eulerAngles.z);
            Direction.Normalize();
            EndPos = StartPos + newDirection.normalized * Distance;
            NextPos = EndPos;
            if (!IsInfinite && Cycles > 0) --Cycles;
        }

        public override void UpdatePosition()
        {
            switch (MovementMode)
            {
                case Mode.None:
                    return;
                case Mode.Once:
                    transform.position = Vector2.MoveTowards(current: transform.position, target: NextPos, Speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, NextPos) < 0.01f)
                    {
                        MovementMode = Mode.None;
                    }
                    break;
                case Mode.Continuous:
                    transform.Translate(Speed * Time.deltaTime * Direction, RelativeSpace);
                    break;
                case Mode.Patrol:
                    if (Cycles >= 0 || IsInfinite)
                    {
                        Speed = HasConstantSpeed ? Speed : NextPos == EndPos ? OnwardSpeed : ReturnSpeed;
                        transform.position = Vector2.MoveTowards(current: transform.position, target: NextPos, Speed * Time.deltaTime);
                        if (Vector2.Distance(transform.position, NextPos) < 0.01f)
                        {
                            isTryingToChangePosition = true;
                        }
                        if (isTryingToChangePosition) TryToChangePosition();
                    }
                    else
                    {
                        MovementMode = Mode.None;
                    }
                    break;
            }
            //Update NextPos in case some values have changed during runtime
            if (RelativeSpace == Space.Self)
            {
                EndPos = StartPos + Direction.RotatedByAngleZ(transform.rotation.eulerAngles.z) * Distance;
                if (NextPos != StartPos) NextPos = EndPos;
            }
        }

        private void TryToChangePosition()
        {
            if (NextPos == EndPos)
            {
                ProcessEndToStartPosTransition();
                return;
            }

            if (NextPos == StartPos)
            {
                ProcessStartToEndPosTransition();
                return;
            }
        }

        private void ProcessEndToStartPosTransition()
        {
            if (HasDelayAtEndPos && currentDelayAtEndPos > 0f)
            {
                currentDelayAtEndPos -= Time.deltaTime;
                return;
            }
            isTryingToChangePosition = false;
            currentDelayAtEndPos = DelayAtEndPos;
            NextPos = StartPos;
            return;
        }

        private void ProcessStartToEndPosTransition()
        {
            if (HasDelayAtStartPos && currentDelayAtStartPos > 0f)
            {
                currentDelayAtStartPos -= Time.deltaTime;
                return;
            }
            isTryingToChangePosition = false;
            currentDelayAtStartPos = DelayAtStartPos;
            if (!IsInfinite) Cycles--;
            NextPos = EndPos;
        }
#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            if (MovementMode != Mode.None && isActiveAndEnabled)
            {
                Color gizmoColor = Color.green;
                gizmoColor.a = 0.25f;
                Gizmos.color = gizmoColor;
                Vector2 direction = RelativeSpace == Space.World ? Direction : Direction.RotatedByAngleZ(transform.rotation.eulerAngles.z);

                if (MovementMode == Mode.Continuous)
                {
                    Color handlesColor = Color.green;
                    handlesColor.a = 0.25f;
                    UnityEditor.Handles.color = handlesColor;
                    UnityEditor.Handles.ArrowHandleCap(1, transform.position, Quaternion.FromToRotation(Vector3.forward, direction), 1.0f, EventType.Repaint);
                }
                else
                {
                    Vector2 startPos = Application.isPlaying ? StartPos : (Vector2)transform.position;
                    Vector2 endPos = Application.isPlaying ? EndPos : (Vector2)transform.position + direction.normalized * Distance;
                    Gizmos.DrawLine(startPos, endPos);
                    Helpers.DrawGizmosWireCube(startPos, transform.rotation, transform.localScale);
                    Helpers.DrawGizmosWireCube(endPos, transform.rotation, transform.localScale);
                }
            }
        }
#endif
    }
}

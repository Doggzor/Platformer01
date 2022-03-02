using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Dungeon
{
    public class RotationalMovement : Movement
    {
        public enum Mode
        {
            None,
            SingleDirection,
            BackAndForth,
            AimAtTarget
        }
        [Range(0, 360)]
        public int Arc = 360;
        public Mode MovementMode = Mode.SingleDirection;
        public bool IsMovingClockwise = true;
        public bool HasConstantSpeed = true;
        public float OnwardSpeed = 0f;
        public float ReturnSpeed = 0f;
        public bool IsInfinite = true;
        public int Cycles = 0;
        public bool HasDelayAtEndPos = false;
        public bool HasDelayAtStartPos = false;
        public float DelayAtEndPos = 0f;
        public float DelayAtStartPos = 0f;
#region AimAtTarget
        public Transform Target;
        public float DetectionRadius;
        public bool IsInstantAim;
        public bool IsInfiniteRange;
        private bool IsTargetInRange => Target != null && Vector2.Distance(transform.position, Target.position) <= DetectionRadius;
        #endregion
        private int positionReachedCounter = 0;
        private float currentDelayAtEndPos = 0f;
        private float currentDelayAtStartPos = 0f;
        private int Direction => Convert.ToInt32(IsMovingClockwise) * -2 + 1;
        private float deltaAngle = 0f;
        private bool IsTargetPositionReached => deltaAngle >= Arc;
        private float startRotation = 0f;
        private bool startClockwise = true;
        public override void Initialize()
        {
            startRotation = transform.rotation.eulerAngles.z;
            startClockwise = IsMovingClockwise;
            Speed = HasConstantSpeed ? Speed : OnwardSpeed;

            currentDelayAtStartPos = DelayAtStartPos;
            currentDelayAtEndPos = DelayAtEndPos;
            if (!IsInfinite && Cycles > 0) --Cycles;
        }

        public override void UpdatePosition()
        {
            switch (MovementMode)
            {
                case Mode.None:
                    return;
                case Mode.SingleDirection:
                    if ( (!IsInfinite || Cycles >= 0) && IsTargetPositionReached)
                    {
                        ProcessDelayAtStartPos();
                    }
                    break;
                case Mode.BackAndForth:
                    if ((!IsInfinite || Cycles >= 0) && IsTargetPositionReached)
                    {
                        CheckForDirectionChange();
                        Speed = HasConstantSpeed ? Speed : positionReachedCounter % 2 == 0 ? OnwardSpeed : ReturnSpeed;
                    }
                    break;
                case Mode.AimAtTarget:
                    if (IsInfiniteRange || IsTargetInRange)
                    {
                        Vector2 targetVector = (Target.position - transform.position);
                        if (IsInstantAim) transform.up = targetVector;
                        else
                        {
                            float rotationZ = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg - 90.0f;
                            rotationZ = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, rotationZ, Speed * Time.deltaTime);
                            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                        }
                    }
                    return;
            }
            if (!IsTargetPositionReached) PerformMovement();
            if (!IsInfinite && Cycles < 0) MovementMode = Mode.None;
        }

 
        private void PerformMovement()
        {
            deltaAngle += Mathf.Abs(Speed * Time.deltaTime);
            transform.Rotate(Direction * Speed * Time.deltaTime * Vector3.forward);
        }
        /// <summary>
        /// Reduces current delay at start position by Time.deltaTime
        /// </summary>
        /// <returns>True if delay reached 0, false otherwise</returns>
        private bool ProcessDelayAtStartPos()
        {
            if (HasDelayAtStartPos && currentDelayAtStartPos > 0f)
            {
                currentDelayAtStartPos -= Time.deltaTime;
                return false;
            }
            deltaAngle = 0f;
            currentDelayAtStartPos = DelayAtStartPos;
            if(!IsInfinite) --Cycles;
            return true;
        }
        /// <summary>
        /// Reduces current delay at end position by Time.deltaTime
        /// </summary>
        /// <returns>True if delay reached 0, false otherwise</returns>
        private bool ProcessDelayAtEndPos()
        {
            if (HasDelayAtEndPos && currentDelayAtEndPos > 0f)
            {
                currentDelayAtEndPos -= Time.deltaTime;
                return false;
            }
            deltaAngle = 0f;
            currentDelayAtEndPos = DelayAtEndPos;
            return true;
        }
        private void CheckForDirectionChange()
        {
            if (IsTargetPositionReached)
            {
                if (positionReachedCounter % 2 == 0)
                {
                    if (ProcessDelayAtEndPos())
                    {
                        ++positionReachedCounter;
                        IsMovingClockwise = !IsMovingClockwise;
                    }
                    return;
                }
                if (ProcessDelayAtStartPos())
                {
                    ++positionReachedCounter;
                    IsMovingClockwise = !IsMovingClockwise;
                }
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (isActiveAndEnabled)
            {
                Color handlesColor = Color.green;
                switch (MovementMode)
                {
                    case Mode.None:
                        break;
                    case Mode.SingleDirection:
                    case Mode.BackAndForth:
                        handlesColor = IsMovingClockwise ? Color.green : Color.yellow;
                        handlesColor.a = 0.35f;
                        UnityEditor.Handles.color = handlesColor;
                        Vector3 normal = IsMovingClockwise ? Vector3.back : Vector3.forward;
                        Vector2 from = Vector2.up.RotatedByAngleZ(transform.rotation.eulerAngles.z);
                        UnityEditor.Handles.ArrowHandleCap(1, transform.position + (Vector3)from * 0.5f, Quaternion.FromToRotation(normal, from.RotatedByAngleZ(90.0f)), 0.5f, EventType.Repaint);
                        if (Application.isPlaying)
                        {
                            normal = startClockwise ? Vector3.back : Vector3.forward;
                            from = Vector2.up.RotatedByAngleZ(startRotation);
                        }
                        UnityEditor.Handles.DrawSolidArc(transform.position, normal, from, Arc, 0.4f);
                        break;
                    case Mode.AimAtTarget:
                        if (!IsInfiniteRange && Target != null)
                        {
                            if (IsTargetInRange) handlesColor = Color.red;
                            handlesColor.a = 0.15f;
                            UnityEditor.Handles.color = handlesColor;
                            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.forward, Vector3.up, 360.0f, DetectionRadius);
                        }
                        break;
                }
            }
        }
#endif
    }
}

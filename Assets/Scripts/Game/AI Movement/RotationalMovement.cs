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
            Perpetual,
            Once,
            Patrol
        }
        [Range(0, 360)]
        public int Arc = 360;
        public Mode MovementMode = Mode.Perpetual;
        public bool IsMovingClockwise = true;

        #region Patrol Specific Variables
        public bool IsInfinite = true;
        public int Cycles = 0;
        public bool HasDelayBetweenPositions = false;
        public bool HasDelayBetweenCycles = false;
        public float DelayBetweenPositions = 0f;
        public float DelayBetweenCycles = 0f;
        #endregion
        private int direction => Convert.ToInt32(IsMovingClockwise) * -2 + 1;
        private float deltaAngle = 0f;
        private bool IsTargetPositionReached => deltaAngle >= Arc;
        public override void Initialize()
        {
        }

        public override void UpdatePosition()
        {
            switch (MovementMode)
            {
                case Mode.None:
                    return;
                case Mode.Perpetual:
                    break;
                case Mode.Once:
                    if (IsTargetPositionReached)
                    {
                        MovementMode = Mode.None;
                    }
                    break;
                case Mode.Patrol:
                    CheckForDirectionChange();
                    break;
            }
            deltaAngle += Mathf.Abs(Speed * Time.deltaTime);
            transform.Rotate(direction * Speed * Time.deltaTime * Vector3.forward);
        }

        private void CheckForDirectionChange()
        {
            if (IsTargetPositionReached)
            {
                deltaAngle = 0f;
                IsMovingClockwise = !IsMovingClockwise;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [CreateAssetMenu(fileName = "Controller", menuName = "Game/Player/Controller")]
    public class PlayerControllerSO : PlayerInputSO
    {
        [SerializeField]
        private PlayerControls controls;
        public override void Read()
        {
            if (isEnabled)
            {
                directionX = Convert.ToInt32(Input.GetKey(controls.RightKey)) - Convert.ToInt32(Input.GetKey(controls.LeftKey));
                if (Input.GetKeyDown(controls.JumpKey))
                    jumpPressTime = Time.time;
                if (Input.GetKeyUp(controls.JumpKey))
                    jumpReleaseTime = Time.time;
            }
            else
            {
                ResetValues();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerInputController : PlayerInput
    {
        [SerializeField]
        PlayerControls controls = new PlayerControls(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow);
        public override void Read()
        {
            if (isEnabled)
            {
                directionX = Convert.ToInt32(Input.GetKey(controls.rightKey)) - Convert.ToInt32(Input.GetKey(controls.leftKey));
                if (Input.GetKeyDown(controls.jumpKey))
                    jumpPressTime = Time.time;
                if (!Input.GetKey(controls.jumpKey))
                    jumpReleaseTime = Time.time;
            }
            else
            {
                ResetValues();
            }
        }
    }
}

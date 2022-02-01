using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateIdle : PlayerState
    {
        public PlayerStateIdle(Player p) : base(p)
        {
        }

        public override void Animate()
        {
            animator.Play("Idle");
        }

        public override void ProcessInput()
        {
            base.ProcessInput();
            player.Actions.Move();
            if (player.PlayerInput.jumpPressTime + player.Stats.jumpBufferTime >= Time.time)
            {
                player.Actions.Jump();
            }
        }
    }
}

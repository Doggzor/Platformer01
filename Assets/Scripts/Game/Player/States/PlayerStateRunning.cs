using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateRunning : PlayerState
    {
        public PlayerStateRunning(Player p) : base(p)
        {
        }

        public override void Animate()
        {
            animator.Play("Run");
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

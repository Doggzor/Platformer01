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
            if (player.PlayerInput.jumpPressTime + player.Stats.jumpToleranceTime >= Time.time)
            {
                player.Actions.Jump();
                player.StateMachine.SwitchToState(player.StateMachine.Jumping);
                return;
            }
            if (!player.Utilities.IsGrounded)
            {
                player.StateMachine.SwitchToState(player.StateMachine.Falling);
                return;
            }
            if (Mathf.Abs(player.PlayerInput.directionX) > Mathf.Epsilon)
            {
                player.StateMachine.SwitchToState(player.StateMachine.Running);
            }
        }
    }
}

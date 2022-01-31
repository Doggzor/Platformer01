using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateFalling : PlayerState
    {
        private float timeLastGrounded = -1f;
        public PlayerStateFalling(Player p) : base(p)
        {
        }
        public override void OnEnter()
        {
            if (player.StateMachine.previousState == player.StateMachine.Running ||
                player.StateMachine.previousState == player.StateMachine.Idle)
            {
                timeLastGrounded = Time.time;
            }
            rb.gravityScale = player.Stats.gravityScale * player.Stats.gravityFallMultiplier;
        }
        public override void Animate()
        {
            animator.Play("Falling");
        }

        public override void ProcessInput()
        {
            base.ProcessInput();
            if (timeLastGrounded + player.Stats.coyoteTime >= Time.time &&
                player.PlayerInput.jumpPressTime > timeLastGrounded)
            {
                player.Actions.Jump();
                player.StateMachine.SwitchToState(player.StateMachine.Jumping);
                return;
            }
            player.Actions.Move();
            if (player.Utilities.IsGrounded)
            {
                if (Mathf.Abs(player.PlayerInput.directionX) > Mathf.Epsilon)
                {
                    player.StateMachine.SwitchToState(player.StateMachine.Running);
                    return;
                }
                player.StateMachine.SwitchToState(player.StateMachine.Idle);
                return;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateJumping : PlayerState
    {
        private float timeSinceJump = -1f;
        public PlayerStateJumping(Player p) : base(p)
        {
        }

        public override void Animate()
        {
            animator.Play("Jump");
        }
        public override void OnEnter()
        {
            base.OnEnter();
            timeSinceJump = Time.time;
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.PlayerInput.jumpReleaseTime > timeSinceJump)
            {
                rb.gravityScale = player.Stats.gravityScale * player.Stats.gravityJumpCutMultiplier;
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
            if (rb.velocity.y < 0f)
            {
                player.StateMachine.SwitchToState(player.StateMachine.Falling);
                return;
            }
        }
    }
}

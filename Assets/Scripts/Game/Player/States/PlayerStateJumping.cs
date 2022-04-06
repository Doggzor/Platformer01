using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateJumping : PlayerStateAir
    {
        private float timeSinceJump = -1f;
        public PlayerStateJumping(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}

        public override void Animate()
        {
            animator.Play("Jump");
        }
        public override void OnEnter()
        {
            base.OnEnter();
            timeSinceJump = Time.time;
            player.PlayerActions.Jump();
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.PlayerInput.jumpReleaseTime > timeSinceJump)
            {
                player.PlayerActions.SetGravity(player.Stats.gravityScale * player.Stats.gravityJumpCutMultiplier);
            }
            if (rb.velocity.y < 0f)
            {
                stateMachine.SwitchToState(stateMachine.Falling);
            }
        }
    }
}

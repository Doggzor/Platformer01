using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateFalling : PlayerStateAir
    {
        public PlayerStateFalling(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}
        public override void OnEnter()
        {
            player.PlayerActions.SetGravity(player.Stats.gravityScale * player.Stats.gravityFallMultiplier);
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override void Animate()
        {
            animator.Play("Falling");
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.Utilities.TimeLastGrounded + player.Stats.coyoteTime >= Time.time &&
                player.PlayerInput.jumpPressTime > player.Utilities.TimeLastGrounded)
            {
                stateMachine.SwitchToState(stateMachine.Jumping);
            }
        }
        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -player.Stats.maxFallSpeed));
        }
    }
}

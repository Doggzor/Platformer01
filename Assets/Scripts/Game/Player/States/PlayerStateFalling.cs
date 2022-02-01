using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateFalling : PlayerState
    {
        private float timeLastGrounded = Mathf.Infinity;
        public PlayerStateFalling(Player p) : base(p)
        {
        }
        public override void OnEnter()
        {
            if (player.StateMachine.previousState != player.StateMachine.Jumping)
            {
                timeLastGrounded = Time.time;
            }
            rb.gravityScale = player.Stats.gravityScale * player.Stats.gravityFallMultiplier;
        }
        public override void OnExit()
        {
            timeLastGrounded = Mathf.Infinity;
        }
        public override void Animate()
        {
            animator.Play("Falling");
        }

        public override void ProcessInput()
        {
            base.ProcessInput();
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -player.Stats.maxFallSpeed));
            if (timeLastGrounded + player.Stats.coyoteTime >= Time.time &&
                player.PlayerInput.jumpPressTime > timeLastGrounded)
            {
                player.Actions.Jump();
            }
            player.Actions.Move();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateIdle : PlayerStateGrounded
    {
        public PlayerStateIdle(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}
        public override void OnEnter()
        {
            base.OnEnter();
            rb.velocity = Vector2.zero;
        }
        public override void ProcessInput()
        {
            if (player.PlayerInput.directionX != 0)
            {
                stateMachine.SwitchToState(stateMachine.Running);
            }
            //Important to first check for running and then for Jumping/Falling to prevent
            //some unwanted behaviour with simultanious pressing of the jump and move key
            base.ProcessInput();
        }

        public override void Animate()
        {
            animator.Play("Idle");
        }
    }
}

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
            base.ProcessInput();
            if (player.PlayerInput.directionX != 0)
            {
                stateMachine.SwitchToState(stateMachine.Running);
            }
        }

        public override void Animate()
        {
            animator.Play("Idle");
        }
    }
}

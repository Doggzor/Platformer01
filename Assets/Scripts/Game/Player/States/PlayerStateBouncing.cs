using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateBouncing : PlayerStateAir
    {
        private float bounceForce = -1f;
        public PlayerStateBouncing(PlayerStateMachine stateMachine, Player p, float bounceForce) : base(stateMachine, p)
        {
            this.bounceForce = bounceForce;
        }

        public override void Animate()
        {
            animator.Play("Jump");
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.PlayerActions.Bounce(bounceForce);
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (rb.velocity.y < 0f)
            {
                stateMachine.SwitchToState(stateMachine.Falling);
            }
        }
    }
}

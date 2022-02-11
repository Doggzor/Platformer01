using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateRunning : PlayerStateGrounded
    {
        public PlayerStateRunning(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.PlayerInput.directionX == 0)
            {
                stateMachine.SwitchToState(stateMachine.Idle);
            }
        }
        public override void UpdatePhysics()
        {
            player.Actions.Move();
        }
        public override void Animate()
        {
            animator.Play("Run");
        }
    }
}

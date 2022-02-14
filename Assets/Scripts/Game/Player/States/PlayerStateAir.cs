using UnityEngine;

namespace Dungeon
{
    public class PlayerStateAir : PlayerState
    {
        public PlayerStateAir(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.Utilities.IsGrounded && rb.velocity.y < 0.01f)
            {
                stateMachine.SwitchToState(player.PlayerInput.directionX == 0 ? stateMachine.Idle : stateMachine.Running);
            }
        }

        public override void UpdatePhysics()
        {
            player.Actions.Move();
        }

    }
}

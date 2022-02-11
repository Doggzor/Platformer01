using UnityEngine;

namespace Dungeon
{
    public class PlayerStateAir : PlayerState
    {
        public PlayerStateAir(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.Utilities.IsGrounded && rb.velocity.y < 0.1f)
            {
                stateMachine.SwitchToState(Mathf.Abs(player.PlayerInput.directionX) > 0.1f ? stateMachine.Running : stateMachine.Idle);
            }
        }

        public override void UpdatePhysics()
        {
            player.Actions.Move();
        }

    }
}

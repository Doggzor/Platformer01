using UnityEngine;

namespace Dungeon
{
    public class PlayerStateGrounded : PlayerState
    {
        public PlayerStateGrounded(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}
        public override void OnEnter()
        {
            base.OnEnter();
            player.Utilities.TimeLastGrounded = Mathf.Infinity;
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            if (player.PlayerInput.jumpPressTime + player.Stats.jumpBufferTime >= Time.time)
            {
                stateMachine.SwitchToState(stateMachine.Jumping);
            }
            else if (!player.Utilities.IsGrounded)
            {
                stateMachine.SwitchToState(stateMachine.Falling);
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            player.Utilities.TimeLastGrounded = Time.time;
        }
    }
}

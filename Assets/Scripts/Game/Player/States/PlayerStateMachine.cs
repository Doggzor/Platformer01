using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateMachine
    {
        private Player player;
        private Rigidbody2D rb;
        public PlayerState currentState {get;private set;}
        public PlayerState previousState { get; private set; }
        public PlayerState Idle { get; private set; }
        public PlayerState Running { get; private set; }
        public PlayerState Jumping { get; private set; }
        public PlayerState Falling { get; private set; }
        public PlayerState Dead { get; private set; }

        public PlayerStateMachine(Player p)
        {
            player = p;
            rb = p.GetComponent<Rigidbody2D>();

            Idle = new PlayerStateIdle(p);
            Running = new PlayerStateRunning(p);
            Jumping = new PlayerStateJumping(p);
            Falling = new PlayerStateFalling(p);
            Dead = new PlayerStateDead(p);

            currentState = Idle;
        }
        public void DetectState()
        {
            if (currentState == Dead)
                return;
            PlayerState s = currentState;
            if (player.Utilities.IsGrounded)
            {
                s = Mathf.Abs(player.PlayerInput.directionX) > Mathf.Epsilon ? Running : Idle;
            }
            else
            {
                if (rb.velocity.y > 0.1f)
                    s = Jumping;
                else if (rb.velocity.y < 0.1f)
                    s = Falling;
            }
            SwitchToState(s);
        }
        public void SwitchToState(PlayerState state)
        {
            if (currentState == state)
                return;
            currentState.OnExit();
            previousState = currentState;
            currentState = state;
            currentState.OnEnter();
        }
    }
}

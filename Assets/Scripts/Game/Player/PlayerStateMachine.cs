using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateMachine
    {
        public PlayerState CurrentState {get; private set;}
        public PlayerState PreviousState { get; private set; }
        public PlayerState Idle { get; private set; }
        public PlayerState Running { get; private set; }
        public PlayerState Jumping { get; private set; }
        public PlayerState Falling { get; private set; }
        public PlayerState Dead { get; private set; }
        public PlayerState DebugState { get; private set; }

        public PlayerStateMachine(Player p)
        {
            Idle = new PlayerStateIdle(this, p);
            Running = new PlayerStateRunning(this, p);
            Jumping = new PlayerStateJumping(this, p);
            Falling = new PlayerStateFalling(this, p);
            Dead = new PlayerStateDead(this, p);
            DebugState = new PlayerStateDebug(this, p);

            CurrentState = Idle;
        }
        public void SwitchToState(PlayerState state)
        {
            if (CurrentState == state)
                return;
            CurrentState.OnExit();
            PreviousState = CurrentState;
            CurrentState = state;
            CurrentState.OnEnter();
        }
    }
}

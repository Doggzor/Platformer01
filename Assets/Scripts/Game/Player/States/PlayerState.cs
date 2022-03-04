using UnityEngine;

namespace Dungeon
{
    public abstract class PlayerState
    {
        protected Player player;
        protected Rigidbody2D rb;
        protected Animator animator;
        protected PlayerStateMachine stateMachine;
        public PlayerState(PlayerStateMachine stateMachine, Player p)
        {
            player = p;
            rb = p.GetComponent<Rigidbody2D>();
            animator = p.GetComponent<Animator>();
            this.stateMachine = stateMachine;
        }
        public virtual void ProcessInput()
        {
            if (player.Utilities.HasFacingDirectionChanged)
            {
                player.Actions.Flip();
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                stateMachine.SwitchToState(stateMachine.DebugState);
                return;
            }
        }
        public virtual void UpdatePhysics()
        {
        }
        public virtual void Animate()
        {
        }
        public virtual void OnEnter()
        {
            player.Actions.SetGravity(player.Stats.gravityScale);
        }
        public virtual void OnExit()
        {
        }
    }
}

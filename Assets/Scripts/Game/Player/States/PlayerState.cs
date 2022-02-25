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
        public virtual void HandleTriggerCollisions(Collider2D collision)
        {
            if (collision.TryGetComponent<IDanger>(out _))
            {
                player.StateMachine.SwitchToState(player.StateMachine.Dead);
            }
            else if (collision.TryGetComponent(out IPickable pickableObject))
            {
                pickableObject.OnPickUp();
            }
            else if (collision.TryGetComponent(out IInteractable interactableObject))
            {
                interactableObject.OnInteraction(player);
            }
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

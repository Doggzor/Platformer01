using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public abstract class PlayerState
    {
        protected Player player;
        protected Rigidbody2D rb;
        protected Animator animator;
        public PlayerState(Player p)
        {
            player = p;
            rb = p.GetComponent<Rigidbody2D>();
            animator = p.GetComponent<Animator>();
        }
        public virtual void ProcessInput()
        {
            if (player.Utilities.HasFacingDirectionChanged)
            {
                player.Actions.Flip();
            }
        }
        public abstract void Animate();
        public virtual void HandleTriggerCollisions(Collider2D collision)
        {
            if (collision.TryGetComponent<IDanger>(out _))
            {
                player.StateMachine.SwitchToState(player.StateMachine.Dead);
            }
            else if (collision.TryGetComponent(out IPickable obj))
            {
                obj.OnPickUp();
            }
        }
        public virtual void OnEnter()
        {
            rb.gravityScale = player.Stats.gravityScale;
        }
        public virtual void OnExit()
        {
            return;
        }
    }
}

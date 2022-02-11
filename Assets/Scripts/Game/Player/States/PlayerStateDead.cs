using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateDead : PlayerState
    {
        public PlayerStateDead(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) { }
        public override void Animate()
        {
            //animator.Play("Death");
        }
        public override void ProcessInput()
        {
        }
        public override void UpdatePhysics()
        {
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.StartCoroutine(player.Actions.Co_TriggerDeath());
        }

        public override void HandleTriggerCollisions(Collider2D collision)
        {
        }

    }
}

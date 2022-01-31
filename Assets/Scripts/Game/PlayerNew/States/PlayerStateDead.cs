using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerStateDead : PlayerState
    {
        public PlayerStateDead(Player p) : base(p) { }
        public override void Animate()
        {
            //animator.Play("Death");
        }
        public override void ProcessInput()
        {
            return;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            player.StartCoroutine(player.Actions.Co_TriggerDeath());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dungeon
{
    public class PlayerStateDebug : PlayerState
    {
        public PlayerStateDebug(Player p) : base(p)
        {
        }

        public override void Animate()
        {
            return;
        }
        public override void ProcessInput()
        {
            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
            player.transform.Translate(dir * Time.deltaTime * player.Stats.speed);
        }
        public override void HandleTriggerCollisions(Collider2D collision)
        {
            return;
        }
        public override void OnEnter()
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
        public override void OnExit()
        {
            player.GetComponent<BoxCollider2D>().enabled = true;
            rb.velocity = Vector2.zero;
        }
    }
}


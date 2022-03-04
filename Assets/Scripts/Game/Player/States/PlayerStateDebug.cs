using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dungeon
{
    public class PlayerStateDebug : PlayerState
    {
        public PlayerStateDebug(PlayerStateMachine stateMachine, Player p) : base(stateMachine, p) {}

        public override void Animate()
        {
        }
        public override void UpdatePhysics()
        {
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
            player.transform.Translate(dir * Time.deltaTime * player.Stats.speed);
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                stateMachine.SwitchToState(stateMachine.Idle);
                return;
            }
        }
        public override void OnEnter()
        {
            player.Actions.SetGravity(0f);
            rb.velocity = Vector2.zero;
            player.GetComponent<BoxCollider2D>().enabled = false;
        }
        public override void OnExit()
        {
            player.GetComponent<BoxCollider2D>().enabled = true;
        }

    }
}


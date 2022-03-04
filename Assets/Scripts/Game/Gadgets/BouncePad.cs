using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class BouncePad : MonoBehaviour, IInteractable
    {
        [SerializeField] private float bounceForce;
        private Animation anim;
        private void Awake()
        {
            anim = GetComponent<Animation>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                OnInteract(player);
            }
        }
        public void OnInteract(Player caller)
        {
            float callerBottomPoint = caller.transform.position.y - caller.GetComponent<Collider2D>().bounds.extents.y;
            if (callerBottomPoint > transform.position.y)
            {
                anim.Play();
                caller.StateMachine.SwitchToState(new PlayerStateBouncing(caller.StateMachine, caller, bounceForce));
            }
        }
    }
}

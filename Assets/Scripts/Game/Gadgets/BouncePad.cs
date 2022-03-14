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
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.bounds.min.y > transform.position.y && collider.TryGetComponent(out Player player))
            {
                OnInteract(player);
            }
        }
        public void OnInteract(Player caller)
        {
            anim.Stop();
            anim.Play();
            caller.StateMachine.SwitchToState(new PlayerStateBouncing(caller.StateMachine, caller, bounceForce));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class BouncePad : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private float _bounceForce;

        public void OnInteraction(GameObject caller)
        {
            Rigidbody2D rbCaller = caller.GetComponent<Rigidbody2D>();
            rbCaller.velocity = new Vector2(rbCaller.velocity.x, 0f);
            rbCaller.AddForce(new Vector2(0f, _bounceForce), ForceMode2D.Impulse);
        }
    }
}

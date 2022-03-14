using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class MovingPlatform : MonoBehaviour, IInteractable
    {
        public void OnInteract(Player caller)
        {
            throw new System.NotImplementedException();
        }
        private Collider2D col;
        private void Awake()
        {
            TryGetComponent(out col);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out Player player))
            {
                if (player.GetComponent<Collider2D>().bounds.min.y >= col.bounds.max.y)
                    collision.transform.parent = transform;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out Player player))
                player.transform.parent = player.Utilities.PlayerBaseParent;
        }
    }
}

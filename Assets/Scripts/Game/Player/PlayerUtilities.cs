using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerUtilities
    {
        private Player player;
        private BoxCollider2D boxCollider;
        private LayerMask groundLayer;
        public bool IsGrounded => Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0f, Vector2.down, 0.05f, groundLayer);
        public bool HasFacingDirectionChanged => Mathf.Abs(player.transform.localScale.x + player.PlayerInput.directionX) < 1.0f;
        public PlayerUtilities(Player p)
        {
            player = p;
            boxCollider = player.GetComponent<BoxCollider2D>();
            groundLayer = LayerMask.GetMask("Ground");
        }
    }
}

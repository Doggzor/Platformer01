using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class PlayerUtilities
    {
        public Transform PlayerBaseParent { get; private set; }
        private LayerMask groundLayer;
        private Player player;
        private BoxCollider2D boxCollider;
        private Vector3 groundCheckOffset;
        private Vector2 groundCheckSize;
        public float TimeLastGrounded { get; set; } = Mathf.Infinity;
        public bool IsGrounded => Physics2D.OverlapBox(boxCollider.bounds.center + groundCheckOffset, groundCheckSize, 0f, groundLayer);
        public bool HasFacingDirectionChanged => Mathf.Abs(player.transform.localScale.x + player.PlayerInput.directionX) < 1.0f;
        public PlayerUtilities(Player p)
        {
            player = p;
            groundLayer = p.GroundLayer;
            PlayerBaseParent = player.transform.parent;
            boxCollider = player.GetComponent<BoxCollider2D>();
            groundCheckOffset = Vector2.down * boxCollider.bounds.extents.y;
            groundCheckSize = new Vector2(boxCollider.size.x * 0.95f, 0.05f);
        }
    }
}

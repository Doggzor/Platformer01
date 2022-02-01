using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [System.Serializable]
    public class PlayerStats
    {
        public PlayerStats() { }
        public PlayerStats(Player p)
        {
            gravityScale = p.GetComponent<Rigidbody2D>().gravityScale;
            acceleration = p.Stats.acceleration;
            speed = p.Stats.speed;
            jumpForce = p.Stats.jumpForce;
            maxFallSpeed = p.Stats.maxFallSpeed;
            jumpBufferTime = p.Stats.jumpBufferTime;
            coyoteTime = p.Stats.coyoteTime;
            gravityScale = p.Stats.gravityScale;
            gravityFallMultiplier = p.Stats.gravityFallMultiplier;
            gravityJumpCutMultiplier = p.Stats.gravityJumpCutMultiplier;
        }
        public float acceleration = 2f;
        public float speed = 12f;
        public float jumpForce = 15f;
        public float maxFallSpeed = 15f;
        public float jumpBufferTime = 0.15f;
        public float coyoteTime = 0.07f;
        public float gravityScale = 1f;
        public float gravityFallMultiplier = 1.5f;
        public float gravityJumpCutMultiplier= 2.5f;

    }
}

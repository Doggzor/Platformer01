using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [System.Serializable]
    public class PlayerStats : MonoBehaviour
    {
        [Space]
        [Header("Movement")]
        public float speed = 7f;
        public float acceleration = 1.2f;
        [Space]
        [Header("Jump")]
        public float jumpForce = 13f;
        public float maxFallSpeed = 13f;
        public float jumpBufferTime = 0.1f;
        public float coyoteTime = 0.1f;
        [Space]
        [Header("Gravity")]
        public float gravityFallMultiplier = 1.5f;
        public float gravityJumpCutMultiplier= 2.5f;
        [HideInInspector]
        public float gravityScale = 1f;

    }
}

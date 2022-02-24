using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Projectile : MonoBehaviour, IDanger
    {
        [Tooltip("Time in seconds before the projectile is destroyed")]
        [Min(0f)]
        [SerializeField] private float lifetime;
        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}

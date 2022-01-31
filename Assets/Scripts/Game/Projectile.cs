using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {
    public class Projectile : MonoBehaviour, IDanger
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}

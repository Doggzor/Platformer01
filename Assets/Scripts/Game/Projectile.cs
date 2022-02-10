using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Projectile : MonoBehaviour, IDanger
    {
        private Movement movement;
        private void Awake()
        {
            movement = GetComponent<Movement>();
        }
    }
}

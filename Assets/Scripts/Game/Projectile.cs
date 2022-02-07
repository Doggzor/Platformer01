using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Projectile : MonoBehaviour, IDanger, IMoving
    {
        private Movement movement;
        private void Awake()
        {
            movement = GetComponent<Movement>();
        }
    }
}

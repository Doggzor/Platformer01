using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] private Projectile projectile;
        private float timer;
        void Awake()
        {
            timer = fireRate;
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Fire();
            }
        }

        private void Fire()
        {
            timer = fireRate;
            Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, 90.0f));
        }
    }
}

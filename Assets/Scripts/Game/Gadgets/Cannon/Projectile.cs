using UnityEngine;

namespace Dungeon
{
    public class Projectile : MonoBehaviour, IDanger
    {
        [SerializeField, Min(0f), Tooltip("Time in seconds before the projectile is destroyed")]
        private float lifetime;
        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}

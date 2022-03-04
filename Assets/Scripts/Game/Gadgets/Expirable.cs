using UnityEngine;

namespace Dungeon
{
    public class Expirable : MonoBehaviour
    {
        [SerializeField, Min(0f), Tooltip("Time in seconds before the object is destroyed")]
        private float lifetime;
        private void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}

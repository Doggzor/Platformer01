using UnityEngine;

namespace Dungeon
{
    public class PickableObjectBase : MonoBehaviour, IPickable
    {
        public virtual void OnPickUp()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnPickUp();
            }
        }
    }
}

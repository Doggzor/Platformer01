using UnityEngine;

namespace Dungeon
{
    public abstract class PickableObjectBase : MonoBehaviour, IPickable
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>())
            {
                OnPickUp();
            }
        }
        public virtual void OnPickUp()
        {
            Destroy(gameObject);
        }
    }
}

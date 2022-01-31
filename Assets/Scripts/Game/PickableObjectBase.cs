using UnityEngine;

namespace Dungeon
{
    public abstract class PickableObjectBase : MonoBehaviour, IPickable
    {
        public virtual void OnPickUp()
        {
            Destroy(gameObject);
        }
    }
}

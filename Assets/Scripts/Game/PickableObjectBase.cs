using UnityEngine;

namespace Dungeon
{
    public class PickableObjectBase : MonoBehaviour, IPickable
    {
        public virtual void OnPickUp()
        {
            Destroy(gameObject);
        }
    }
}

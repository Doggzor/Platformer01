using UnityEngine;

namespace Dungeon
{
    public interface IInteractable
    {
        void OnInteraction(GameObject caller);
    }
}

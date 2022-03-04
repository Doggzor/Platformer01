using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Game/Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

        public void Raise()
        {
            foreach (var listener in _listeners)
            {
                listener.OnEventRaised();
            }
        }
        public void Register(GameEventListener listener) => _listeners.Add(listener);
        public void Unregister(GameEventListener listener) => _listeners.Remove(listener);
    }
}

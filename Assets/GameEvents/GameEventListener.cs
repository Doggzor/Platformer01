using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dungeon
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent _action;

        public void OnEventRaised() => _action.Invoke();
        private void OnEnable() => _gameEvent.Register(this);
        private void OnDisable() => _gameEvent.Unregister(this);
    }
}
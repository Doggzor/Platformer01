using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon {
    public class Goal : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) Actions.PlayerReachedGoal?.Invoke();
        }
    }
}

using UnityEngine;

namespace Dungeon {
    [RequireComponent(typeof(Collider2D))]
    public class KillTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                player.StateMachine.SwitchToState(player.StateMachine.Dead);
            }
        }
    }
}

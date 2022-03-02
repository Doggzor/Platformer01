using UnityEngine;

namespace Dungeon
{
    public class test : MonoBehaviour
    {
        [SerializeField]
        private MovePattern movement;
        private void Start()
        {
            movement.Initialize(transform);
        }
        private void Update()
        {
            movement.UpdatePosition(transform);
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(isActiveAndEnabled && movement)
                movement.DrawRelatedGizmos(transform);
        }
#endif
    }
}

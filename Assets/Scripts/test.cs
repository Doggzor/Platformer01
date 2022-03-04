using UnityEngine;

namespace Dungeon
{
    public class test : MonoBehaviour
    {
        [SerializeField]
        private MovePattern movement;
        public int id;
        private void OnEnable()
        {
        }
        private void OnDisable()
        {
        }
        private void Start()
        {
            movement.Initialize(transform);
        }
        private void Update()
        {
            movement.UpdatePosition(transform);
        }

        void RandomizeColor(int id)
        {
            if(this.id == id)
            {
                Color c = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                GetComponent<SpriteRenderer>().color = c;
            }
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

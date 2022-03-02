using UnityEngine;

namespace Dungeon
{
    [CreateAssetMenu(fileName = "Continuous Movement", menuName = "Game/Move Pattern/Linear/Continuous")]
    public class SO_LinearMovement : MovePattern
    {
        [field: SerializeField] public Vector2 Direction { get; private set; }
        [field: SerializeField] public Space RelativeSpace { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }

        public override void Initialize(Transform transform)
        {

        }

        public override void UpdatePosition(Transform transform)
        {
            transform.Translate(Speed * Time.deltaTime * Direction, RelativeSpace);
        }
        public override void DrawRelatedGizmos(Transform transform)
        {
            Vector2 direction = RelativeSpace == Space.World ? Direction : Direction.RotatedByAngleZ(transform.rotation.eulerAngles.z);
            Color handlesColor = Color.green;
            handlesColor.a = 0.35f;
            UnityEditor.Handles.color = handlesColor;
            UnityEditor.Handles.ArrowHandleCap(1, transform.position, Quaternion.FromToRotation(Vector3.forward, direction), 1.0f, EventType.Repaint);
        }
    }
}

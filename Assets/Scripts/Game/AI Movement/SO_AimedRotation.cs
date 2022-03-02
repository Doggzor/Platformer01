using UnityEngine;

namespace Dungeon
{
    [CreateAssetMenu(fileName = "Aimed Rotation", menuName = "Game/Move Pattern/Rotational/Aimed")]
    public class SO_AimedRotation : MovePattern
    {
        public Vector2 Target;
        [field: SerializeField, Tooltip("Aim at target as soon as it is in range")]
        public bool IsInstantLock { get; private set; }
        [field: SerializeField, Tooltip("Target will always be detected")]
        public bool IsInfiniteRange { get; private set; }
        [field: SerializeField] public float DetectionRadius { get; private set; }

        public override void Initialize(Transform transform)
        {
        }

        public override void UpdatePosition(Transform transform)
        {
            Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsInfiniteRange || IsTargetInRange(transform))
            {
                Vector2 targetVector = (Target - (Vector2)transform.position);
                if (IsInstantLock)
                {
                    transform.up = targetVector;
                    return;
                }
                float rotationZ = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg - 90.0f;
                rotationZ = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, rotationZ, Speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            }
        }
        public override void DrawRelatedGizmos(Transform transform)
        {
            if (!IsInfiniteRange)
            {
                Color gizmosColor = Color.green;
                if (IsTargetInRange(transform))
                {
                    gizmosColor = Color.red;
                }
                gizmosColor.a = 0.15f;

                Gizmos.color = gizmosColor;
                Gizmos.DrawSphere(transform.position, DetectionRadius);
            }
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Target, 0.2f);
        }

        private bool IsTargetInRange(Transform transform) => Vector2.Distance(transform.position, Target) <= DetectionRadius;
    }
}

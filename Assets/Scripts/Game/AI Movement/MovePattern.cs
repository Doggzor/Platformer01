using UnityEngine;

public abstract class MovePattern : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    public abstract void Initialize(Transform transform);
    public abstract void UpdatePosition(Transform transform);
    public abstract void DrawRelatedGizmos(Transform transform);

}

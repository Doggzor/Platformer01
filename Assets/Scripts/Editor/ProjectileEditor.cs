using UnityEditor;

namespace Dungeon
{
    [CustomEditor(typeof(Projectile))]
    public class ProjectileEditor : Editor
    {
        Projectile p;
        private void OnEnable()
        {
            p = (Projectile)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (p.GetComponent<Movement>() == null)
            {
                EditorGUILayout.HelpBox("Projectile objects require a Movement component", MessageType.Error);
            }
        }
    }
}

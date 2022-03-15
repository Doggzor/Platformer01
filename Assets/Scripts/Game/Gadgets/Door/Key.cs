using UnityEngine;

namespace Dungeon
{
    public class Key : PickableObjectBase
    {
        private Door door;
        private GameObject particles;
        private void Awake()
        {
            door = GetComponentInParent<Door>();
            particles = transform.Find("Particles").gameObject;
            var particleSettings = particles.GetComponent<ParticleSystem>().main;
            particleSettings.startColor = transform.Find("Sprite").GetComponent<SpriteRenderer>().color;
        }
        
        public override void OnPickUp()
        {
            particles.SetActive(true);
            particles.transform.parent = null;
            door.FadeOut();
            base.OnPickUp();
        }
    }
}

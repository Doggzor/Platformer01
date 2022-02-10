namespace Dungeon
{
    public class Key : PickableObjectBase
    {
        private Door door;
        private void Awake()
        {
            door = GetComponentInParent<Door>();
        }
        
        public override void OnPickUp()
        {
            door.FadeOut();
            base.OnPickUp();
        }
    }
}

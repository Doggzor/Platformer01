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
            base.OnPickUp();
            door.isFading = true;
        }
    }
}

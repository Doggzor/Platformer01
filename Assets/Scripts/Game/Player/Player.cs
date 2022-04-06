using UnityEngine;

namespace Dungeon
{
    [SelectionBase]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        private PlayerState State => StateMachine.CurrentState;
        public PlayerUtilities Utilities { get; private set; }
        public PlayerActions PlayerActions { get; private set; }

        [SerializeField, Space]
        private PlayerSkinSO Skin;

        [field: SerializeField] public PlayerStatsSO Stats { get; private set; }
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [field: SerializeField] public LayerMask GroundLayer { get; private set; }

        private void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            Utilities = new PlayerUtilities(this);
            PlayerActions = new PlayerActions(this);

            //ApplySkin();
        }
        private void Start()
        {
            StartCoroutine(Utilities.Co_NotifyWhenMovementStarts());
        }
        private void Update()
        {
            PlayerInput.Read();
            State.ProcessInput();
            State.Animate();
        }
        private void FixedUpdate()
        {
            State.UpdatePhysics();
        }

        public void ApplySkin()
        {
            var sprite = transform.Find("Sprite");
            var body = sprite.transform.Find("Body");
            var head = body.transform.Find("Head");
            var eye_front = head.transform.Find("Eye Front");
            var eye_back = head.Find("Eye Back");
            var arm_front = body.transform.Find("Arm Front");
            var arm_back = body.transform.Find("Arm Back");
            var tail = body.transform.Find("Tail");
            var leg_front = sprite.transform.Find("Leg Front");
            var leg_back = sprite.transform.Find("Leg Back");

            head.GetComponent<SpriteRenderer>().sprite = Skin.head;
            eye_front.GetComponent<SpriteRenderer>().sprite = Skin.eye_front;
            eye_back.GetComponent<SpriteRenderer>().sprite = Skin.eye_back;
            body.GetComponent<SpriteRenderer>().sprite = Skin.body;
            arm_front.GetComponent<SpriteRenderer>().sprite = Skin.arm_front;
            arm_back.GetComponent<SpriteRenderer>().sprite = Skin.arm_back;
            tail.GetComponent<SpriteRenderer>().sprite = Skin.tail;
            leg_front.GetComponent<SpriteRenderer>().sprite = Skin.leg_front;
            leg_back.GetComponent<SpriteRenderer>().sprite = Skin.leg_back;
        }
    }
}

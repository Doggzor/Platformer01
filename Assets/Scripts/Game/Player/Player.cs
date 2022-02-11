using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    [RequireComponent(typeof(PlayerStats))]
    public class Player : MonoBehaviour
    {
        public PlayerStateMachine StateMachine { get; private set; }
        private PlayerState State => StateMachine.CurrentState;
        public PlayerUtilities Utilities { get; private set; }
        public PlayerActions Actions { get; private set; }

        [Space]
        [SerializeField]
        private PlayerSkinPrefab skin = null;

        [SerializeField]
        private LayerMask groundLayer;
        public LayerMask GroundLayer { get => groundLayer; }

        public PlayerInput PlayerInput { get; private set; }
        public PlayerStats Stats { get; private set; }

        private void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
            PlayerInput = GetComponent<PlayerInput>();
            Stats = GetComponent<PlayerStats>();
            Utilities = new PlayerUtilities(this);
            Actions = new PlayerActions(this);

            //ApplySkin();
        }
        private void Start()
        {
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            State.HandleTriggerCollisions(collision);
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

            head.GetComponent<SpriteRenderer>().sprite = skin.head;
            eye_front.GetComponent<SpriteRenderer>().sprite = skin.eye_front;
            eye_back.GetComponent<SpriteRenderer>().sprite = skin.eye_back;
            body.GetComponent<SpriteRenderer>().sprite = skin.body;
            arm_front.GetComponent<SpriteRenderer>().sprite = skin.arm_front;
            arm_back.GetComponent<SpriteRenderer>().sprite = skin.arm_back;
            tail.GetComponent<SpriteRenderer>().sprite = skin.tail;
            leg_front.GetComponent<SpriteRenderer>().sprite = skin.leg_front;
            leg_back.GetComponent<SpriteRenderer>().sprite = skin.leg_back;
        }
    }
}

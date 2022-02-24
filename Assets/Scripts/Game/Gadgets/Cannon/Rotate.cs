using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Rotate : MonoBehaviour
    {
        [Tooltip("How fast to rotate (degrees per second)")]
        [Min(0f)]
        [SerializeField] private float speed;
        [Range(0, 360)]
        [SerializeField] private int arc = 360;
        [SerializeField] bool clockwise = true;

        private float midAngle = 0f;
        private float deltaAngle = 0f;
        private float direction => clockwise ? -1f : 1f;
        private void Awake()
        {
            midAngle = transform.rotation.eulerAngles.z + direction * arc * 0.5f;
        }

        private void Update()
        {
            if (arc < 360f)
            {
                CheckForDirectionChenge();
            }
            transform.Rotate(Vector3.forward * speed * Time.deltaTime * direction);
        }

        private void CheckForDirectionChenge()
        {
            float angleDelta = Mathf.Abs(transform.rotation.eulerAngles.z - midAngle);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log($"Current Angle: {transform.rotation.eulerAngles.z}");
                Debug.Log($"Delta Angle: {angleDelta}");
            }
            if (angleDelta >= arc)
            {
                clockwise = !clockwise;
            }
        }

    }
}

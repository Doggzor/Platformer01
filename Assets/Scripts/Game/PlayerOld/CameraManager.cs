using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D rbPlayer;
    private Vector3 offset;
    private Vector3 targetPosition => player.position + offset;
    private float smoothness = 0.008f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        offset = new Vector3(player.localScale.x * 1.5f, 0.75f, -10.0f);

    }
    void Start()
    {
        transform.position = targetPosition;
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        offset.x = (player.localScale.x + rbPlayer.velocity.x * 0.5f) * 1.5f;
        offset.y = 0.75f + rbPlayer.velocity.y * 0.75f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);
    }
}

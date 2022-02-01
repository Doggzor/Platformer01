using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rbPlayer;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //rbPlayer = player.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    void Update()
    {
        FollowPlayer();
    }
    
    void FollowPlayer()
    {
        //TESTING LERP
        //transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + player.transform.localScale.x, player.transform.position.y + (rbPlayer.velocity.y * 0.15f), transform.position.z), 0.03f * (1f + Mathf.Abs(rbPlayer.velocity.y * 0.015f)));
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}

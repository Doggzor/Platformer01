using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    void Update()
    {
        FollowPlayer();
    }
    
    void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}

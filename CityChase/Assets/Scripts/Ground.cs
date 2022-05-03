using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private GameObject player;
    private float playerX;
    private float playerZ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Follows the player's X and Z, but stays at the Y value of 0
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
        transform.position = new Vector3(playerX, 0, playerZ);
    }
}

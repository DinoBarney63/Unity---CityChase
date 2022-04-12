using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private float playerX;
    private float playerZ;
    public GameObject enemyPrefab;
    public int enemyCount;
    public int enemys = 1;
    public float spawnRadius = 85.0f;
    private float spawnPosX;
    private float spawnPosZ;
    public GameObject buildingPrefab;
    public int gridSize = 5;
    public TextMeshProUGUI gameOverText;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        for (int x = -gridSize; x < gridSize; x++)
        {
            for (int z = -gridSize; z < gridSize; z++)
            {
                int spawn = Random.Range(1, 4);
                if (spawn == 1)
                {
                    if (x != 0 && z != 0)
                    {
                        Vector3 spawnPos = new Vector3(x * 20, 15, z * 20);
                        Instantiate(buildingPrefab, spawnPos, enemyPrefab.transform.rotation);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount < enemys)
        {
            Instantiate(enemyPrefab, GenerateSpawnPostion(), enemyPrefab.transform.rotation);
        }
    }
    
    private Vector3 GenerateSpawnPostion()
    {
        int direction = Random.Range(1, 4);
        spawnPosX = Random.Range(playerX - spawnRadius, playerX + spawnRadius);
        spawnPosZ = Random.Range(playerZ - spawnRadius, playerZ + spawnRadius);
        if (direction == 1)
        {
            spawnPosX = playerX + spawnRadius;
        }else if (direction == 2)
        {
            spawnPosZ = playerZ + spawnRadius;
        }
        else if (direction == 3)
        {
            spawnPosX = playerX - spawnRadius;
        }
        else if (direction == 4)
        {
            spawnPosZ = playerZ - spawnRadius;
        }
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}

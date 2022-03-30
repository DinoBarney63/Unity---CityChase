using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 45.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Instantiate(enemyPrefab, GenerateSpawnPostion(), enemyPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenerateSpawnPostion()
    {
        int direction = Random.Range(1, 4);
        if (direction == 1 or direction == 3)
        {
            spawnPosX = Random.Range(-spawnRadius, spawnRadius);
            if (direction == 1):
            {
                spawnPosY = spawnRadius;
            }else:
            {
                spawnPosY = -spawnRadius;
            }
        }else:
        {
            spawnPosY = Random.Range(-spawnRadius, spawnRadius);
            if (direction == 2):
            {
                spawnPosX = spawnRadius;
            }else
            {
                spawnPosX = -spawnRadius;
            }
        }
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosY);
        return randomPos;
    }
}

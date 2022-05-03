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
    public int enemyTotal = 5;
    public float spawnRadius = 85.0f;
    private float spawnPosX;
    private float spawnPosZ;
    public GameObject buildingPrefab;
    public int gridSize = 5;
    private int score;
    public float healthPercentage;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthPrecentageText;
    public TextMeshProUGUI gameOverText;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        score = 0;
        UpdateScore(0);
        healthPercentage = 100;
        UpdateHealthPercentage(0);

        // Spawns the building grid
        for (int x = -gridSize; x < gridSize; x++)
        {
            for (int z = -gridSize; z < gridSize; z++)
            {
                int spawn = Random.Range(1, 4);
                if (spawn == 1)
                {
                    // This condition prevents a building spawning on the player at the beguining
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
        // Gets the players position
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
        // Gets the enemy count and if the enemy count is less that the total required a new enemy is spawned 
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount < enemyTotal)
        {
            Instantiate(enemyPrefab, GenerateSpawnPostion(), enemyPrefab.transform.rotation);
        }
    }
    
    // This is used to generate a spawn position for a enemy
    private Vector3 GenerateSpawnPostion()
    {
        // It starts by generating a number between 1 and 4 to decide which direction the enemy is coming from
        int direction = Random.Range(1, 4);
        // Random generation
        spawnPosX = Random.Range(playerX - spawnRadius, playerX + spawnRadius);
        spawnPosZ = Random.Range(playerZ - spawnRadius, playerZ + spawnRadius);
        // Sets the X or Z coord for the enemy to spawn in the certain direction
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
        // Returns the spawn position
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    // This updates the total enemy count
    public void UpdateEnemies(int enemiesToAdd)
    {
        enemyTotal += enemiesToAdd;
    }

    
    // This triggers when the game is over
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    // This updates the score
    public void UpdateScore(int scoreToAdd)
    {
        // Adjusts the score and displays it
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // This updates the players health
    public void UpdateHealthPercentage(float healthToSubtract)
    {
        // Adjusts the health and displays it
        healthPercentage -= healthToSubtract;
        // If the players health is less than or equal to 0 then the game is over
        if (healthPercentage <= 0)
        {
            healthPercentage = 0;
            GameOver();
        }
        healthPrecentageText.text = "Health: " + Mathf.Round(healthPercentage) + "%";
    }
}

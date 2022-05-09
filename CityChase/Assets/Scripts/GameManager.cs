using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int gridSize = 10;
    private int score;
    private float healthPercentage;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthPrecentageText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI startingText;
    public TextMeshProUGUI restartingText;
    public TextMeshProUGUI howToMoveText;
    public TextMeshProUGUI howToTurnText;
    private bool gameActive = false;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        // Reseting the score and health
        score = 0;
        UpdateScore(0);
        healthPercentage = 100;
        UpdateHealthPercentage(0);

        // Spawns the building grid
        // For each value of x from -gridsize to gridsize
        for (int x = -gridSize; x <= gridSize; x++)
        {
            // For each value of z from -gridsize to gridsize
            for (int z = -gridSize; z <= gridSize; z++)
            {
                // If the values of x and z are both not 0 then...
                if (!(x == 0 && z == 0))
                {
                    // If the absolute value of x or z is bigger than or equal to gridsixe - 3 (for a grid border of 3) then...
                    if ((Mathf.Abs(x) >= gridSize - 3) || (Mathf.Abs(z) >= gridSize - 3))
                    {
                        // Spawn a building
                        Vector3 spawnPos = new Vector3(x * 20, 15, z * 20);
                        Instantiate(buildingPrefab, spawnPos, enemyPrefab.transform.rotation);
                    }else
                    {
                        // If the random number between 1 and 4 is equal to 1 then...
                        int spawn = Random.Range(1, 4);
                        if (spawn == 1)
                        {
                            // Spawn a building
                            Vector3 spawnPos = new Vector3(x * 20, 15, z * 20);
                            Instantiate(buildingPrefab, spawnPos, enemyPrefab.transform.rotation);
                        }
                    }
                }      
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
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
        }else
        {
            // If the game is not active then if the key space is pressed then...
            if(Input.GetKeyDown("space"))
            {
                // If the game hasn't started yet then the game beguins
                if(!gameOver)
                {
                    gameActive = true;
                    titleText.gameObject.SetActive(false);
                    gameOverText.gameObject.SetActive(false);
                    startingText.gameObject.SetActive(false);
                    howToMoveText.gameObject.SetActive(false);
                    howToTurnText.gameObject.SetActive(false);
                }
                else
                {
                    // If it has ended then the scene is reset
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                
            }
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
        // The game is turned off and the title is shown
        gameActive = false;
        gameOver = true;
        titleText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        restartingText.gameObject.SetActive(true);
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
        // If the players health is less than or equal to 0 then the game is over if it is over 100 then it is set to 100
        if (healthPercentage <= 0)
        {
            healthPercentage = 0;
            GameOver();
        }else if(healthPercentage > 100)
        {
            healthPercentage = 100;
        }
        healthPrecentageText.text = "Health: " + Mathf.Round(healthPercentage) + "%";
    }
}

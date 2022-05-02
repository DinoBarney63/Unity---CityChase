using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    public bool isAlive = true;
    public int enemyHealth = 5;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy has health (Alive) then it is pointed towards the player and moves in that direction 
        if (isAlive)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.LookAt(player.transform.position);
            enemyRb.AddForce(lookDirection * speed);
        }

        // If the enemy has fallen out of the world then it is destroyed
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            // If the enemy was killed beforehand then the enemy count gose down by 1
            if (!isAlive)
                gameManager.UpdateEnemies(-1);
        }
    }

    // OnCollisionEnter is called when something eneters the enemies collision box
    private void OnCollisionEnter(Collision collision)
    {
        // When the enemy hits a player, obstacle, or enemy then it loses a hit point
        if (collision.gameObject.CompareTag("Player"))
            enemyHealth -= 1;
        else if (collision.gameObject.CompareTag("Obstacle"))
            enemyHealth -= 1;
        else if (collision.gameObject.CompareTag("Enemy"))
            enemyHealth -= 1;

        // If the enemy is out of health it calls a function to call in a additional enemy
        // So there is always a equal amount of enemies alive
        if (enemyHealth < 0 && isAlive)
        {
            gameManager.UpdateEnemies(1);
            isAlive = false;
        }
    }
}

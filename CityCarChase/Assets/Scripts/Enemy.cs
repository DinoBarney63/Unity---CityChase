using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 20.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    private int enemyHealth = 5;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth > 0)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            transform.LookAt(player.transform.position);
            enemyRb.AddForce(lookDirection * speed);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
            if (enemyHealth <= 0)
                gameManager.UpdateEnemies(-1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            enemyHealth -= 1;
        else if (collision.gameObject.CompareTag("Obstacle"))
            enemyHealth -= 1;
        else if (collision.gameObject.CompareTag("Enemy"))
            enemyHealth -= 1;

        if (enemyHealth == 0)
        {
            gameManager.UpdateEnemies(1);
        }
    }
}

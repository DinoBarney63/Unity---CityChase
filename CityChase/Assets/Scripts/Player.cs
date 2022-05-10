using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 25.0f;
    public float turnSpeed = 90.0f;
    private float horizontalInput;
    private float forwardInput;
    private bool gameActive = false;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float playerXRotation;
    private float playerZRotation;
    private GameManager gameManager;
    public bool healthRegenCountingDown = true;
    private float healthRegenCountdownMax = 5;
    public float healthRegenCountdown = 5;
    private float healthRegenTimeMax = 2;
    public float healthRegenTime = 2;
    public float playerHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerHealth = 100;
    }


    // Update is called once per frame
    void Update()
    {
        // Gets the player's input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Gets the player's rotation
        playerXRotation = transform.localRotation.eulerAngles.x;
        playerZRotation = transform.localRotation.eulerAngles.z;

        // If the player's rotation is so the car is up-side down then the game is over
        if (playerXRotation > 80 && playerXRotation < 280)
        {
            gameActive = false;
            gameManager.GameOver();
        }else if (playerZRotation > 80 && playerZRotation < 280)
        {
            gameActive = false;
            gameManager.GameOver();
        }

        // When the game is active the player is moved and rotated baced on the player's input otherwise the game is over
        if (gameActive)
        {
            playerRb.AddForce(playerRb.transform.forward * speed * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

            // Health regen timer
            // If the inital timer is counting down...
            if(healthRegenCountingDown)
            {
                // If the timer is above 0 then the timer is decreased by time
                if (healthRegenCountdown > 0)
                    healthRegenCountdown -= Time.deltaTime;
                else
                {
                    // Otherwise the timer is set to 0 and the inital timer is deactivated
                    healthRegenCountdown = 0;
                    healthRegenCountingDown = false;
                }
            }else
            {
                // Since the initual timer is deactivated then the second timer can run
                // If this timer is above 0 then the timer is decreased by time
                if (healthRegenTime > 0)
                    healthRegenTime -= Time.deltaTime;
                else
                {
                    // Once the timer is up then the timer is reset and the health is increased by 1
                    healthRegenTime = healthRegenTimeMax;
                    gameManager.UpdateHealthPercentage(-1);
                    playerHealth += 1;
                    if (playerHealth > 100)
                        playerHealth = 100;
                }
            }
        }else
        {
            // If space is pressed then the game beguins
            if (Input.GetKeyDown("space"))
            {
                gameActive = true;
            }
        }
    }

    // OnCollisionEnter is called when the player initionaly collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // If the player collided with an obstacle or an enemy then the player loses 1% health and the health regen timers are reset
        if ((collision.gameObject.CompareTag("Obstacle"))||(collision.gameObject.CompareTag("Enemy")))
        {
            gameManager.UpdateHealthPercentage(1);
            healthRegenCountingDown = true;
            healthRegenCountdown = healthRegenCountdownMax;
            healthRegenTime = healthRegenTimeMax;
            playerHealth -= 1;
            if (playerHealth == 0)
                gameActive = false;
        }
    }
}

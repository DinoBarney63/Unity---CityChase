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
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
        }else
        {
            if (Input.GetKeyDown("space"))
            {
                gameActive = true;
            }
        }
    }

    // OnCollisionEnter is called when the player initionaly collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // If the player collided with an obstacle then the player loses 0.5% health
        if (collision.gameObject.CompareTag("Obstacle"))
            gameManager.UpdateHealthPercentage(0.5f);

        // If the player collided with an enemy then the player loses 1% health
        else if (collision.gameObject.CompareTag("Enemy"))
            gameManager.UpdateHealthPercentage(1);
    }
}

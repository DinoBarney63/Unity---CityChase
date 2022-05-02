using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 90.0f;
    public float horizontalInput;
    public float forwardInput;
    public bool gameActive = true;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float playerXRotation;
    public float playerZRotation;
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
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        playerXRotation = transform.localRotation.eulerAngles.x;
        playerZRotation = transform.localRotation.eulerAngles.z;

        if (playerXRotation > 80 && playerXRotation < 280)
            gameActive = false;
            
        else if (playerZRotation > 80 && playerZRotation < 280)
            gameActive = false;
           
            
        if (gameActive)
        {
            playerRb.AddForce(playerRb.transform.forward * speed * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }
        else
        {
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
            gameManager.UpdateHealthPercentage(0.5f);

        else if (collision.gameObject.CompareTag("Enemy"))
            gameManager.UpdateHealthPercentage(1);
    }
}

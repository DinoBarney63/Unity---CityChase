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
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
        else
            gameActive = true;

        if (gameActive)
        {
            playerRb.AddForce(playerRb.transform.forward * speed * forwardInput);
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        }
    }
}

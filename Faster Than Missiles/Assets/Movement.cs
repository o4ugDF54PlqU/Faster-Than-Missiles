using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public bool thrusting = false;
    public SpriteRenderer exhaustSprite;
    public float rotationSpeed = 180f;
    public float drag = 1;
    public float angularDrag = 100;
    public float thrustForce = 1000;
    public float maxVelocity = 10;
    public float maxAngularVelocity = 10;
    private bool turningLeft = false;
    private bool turningRight = false;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.drag = drag;
        playerRigidbody.angularDrag = angularDrag;
    }

    void Update()
    {
        if (thrusting)
        {
            playerRigidbody.AddForce(transform.up * thrustForce * Time.deltaTime);
        }
        else
        {
            
        }

        if (turningLeft)
        {
            playerRigidbody.AddTorque(rotationSpeed * Time.deltaTime);
        }
        if (turningRight)
        {
            playerRigidbody.AddTorque(-rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (playerRigidbody.velocity.magnitude > maxVelocity)
        {
            playerRigidbody.velocity = playerRigidbody.velocity.normalized * maxVelocity;
        }
        if (Mathf.Abs(playerRigidbody.angularVelocity) > maxAngularVelocity)
        {
            if (playerRigidbody.angularVelocity > 0)
            playerRigidbody.angularVelocity = maxAngularVelocity;
            else
            playerRigidbody.angularVelocity = -maxAngularVelocity;
        }
    }

    public void Thrust(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            playerRigidbody.drag = 0.3f;
            thrusting = true;
            exhaustSprite.enabled = true;
        }
        else
        {
            playerRigidbody.drag = drag;
            thrusting = false;
            exhaustSprite.enabled = false;
        }
    }

    public void TurnLeft(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            playerRigidbody.angularDrag = 0.01f;
            turningLeft = true;
        }
        else
        {
            playerRigidbody.angularDrag = angularDrag;
            turningLeft = false;
        }
    }

    public void TurnRight(InputAction.CallbackContext context)
    {
        if (context.action.IsPressed())
        {
            playerRigidbody.angularDrag = 0.01f;
            turningRight = true;
        }
        else
        {
            playerRigidbody.angularDrag = angularDrag;
            turningRight = false;
        }
    }
}
